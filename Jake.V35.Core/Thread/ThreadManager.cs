using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Jake.V35.Core.Logger;

//======================================================//
//			作者中文名:	林国杰				            //
//			英文名:		jake				            //
//			创建时间:	11/11/2016 8:56:57 AM			//
//			创建日期:	2016				            //
//======================================================//
namespace Jake.V35.Core.Thread
{
    /// <summary>
    /// 一个通用的线程管理类
    /// 实现基于线程的异步操作
    ///  ThreadManager threadManger = new ThreadManager(logger);//初始化，指定初始线程数
    ///  threadManger.WorkStarting += WorkStarting;//在轮旋开始前处理
    ///  threadManger.Work += Work;//指定线程执行工作
    /// if (!threadManger.IsStart)
    ///  threadManger.Start();//启动
    /// </summary>
    public class ThreadManager:IDisposable
    {
        private readonly AutoResetEvent _stopAutoResetEvent;
        private readonly ILogger _logger;
        private int _threadAmouont;
        /// <summary>
        /// 当前启动的线程数
        /// </summary>
        public int ThreadAmount
        {
            get { return _threadAmouont; }
            set { _threadAmouont = value; }
        }
        /// <summary>
        /// 线程执行完事件间隔
        /// </summary>
        public int Delay { get; private set; }
        public bool IsStart { get; private set; }
        private bool _waitStop;
        private readonly List<System.Threading.Thread> _threads;
        private event Action _work;
        public event Action Work
        {
            add { _work += value; }
            remove { _work -= value; }
        }

        private event Action _workStarting;
        /// <summary>
        /// 线程循环前只执行一次
        /// </summary>
        public event Action WorkStarting
        {
            add { _workStarting += value; }
            remove { _workStarting -= value; }
        }

        private event Action<object, EventArgs> _threadAmountChanged;
        public event Action<object, EventArgs> ThreadAmountChanged
        {
            add { _threadAmountChanged += value; }
            remove { _threadAmountChanged -= value; }
        }

        public ThreadManager(ILogger logger, int defaultThreadCount = 1, int delay = 10)
            : this(defaultThreadCount, delay)
        {
            _logger = logger ;
        }

        public ThreadManager(int defaultThreadCount = 1, int delay = 10)
        {
            Delay = delay;
            _threads = new List<System.Threading.Thread>(defaultThreadCount);
            _threadAmouont = defaultThreadCount;
            AddThread(defaultThreadCount, false);
            IsStart = false;
            _waitStop = false;
            _stopAutoResetEvent = new AutoResetEvent(false);
        }
        /// <summary>
        /// 开始线程
        /// </summary>
        public void Start()
        {
            CheckDispose();
            lock (_threads)
            {
                if (IsStart)
                {
                    _logger.WriteError("线程已经启动,重复启动线程");
                    throw new Exception("Thread Manager is working！");
                }
                _threads.ForEach(t => t.Start());
                IsStart = true;
                _logger.WriteError("线程启动成功");
            }
        }
        /// <summary>
        /// 停止线程
        /// </summary>
        public void Stop()
        {
            CheckDispose();
            lock (_threads)
            {
                //已经再等待则直接返回。
                if (_waitStop)
                {
                    _logger.WriteInfo("线程正在等待当前工作处理完成后停止"); 
                    return;
                }
                if (!IsStart)
                {
                    _logger.WriteError("线程未启动，无需停止"); 
                    throw new Exception("Thread Manager is stopping！");
                }
                _waitStop = true;
                _logger.WriteError("发送停止信号，等待所有线程执行完毕");
                //等待所有线程结束
                _stopAutoResetEvent.WaitOne();
                _logger.WriteInfo("线程停止成功");
                _waitStop = false;
            }
        }
        /// <summary>
        /// 线程执行
        /// </summary>
        private void OnWork()
        {
            CheckDispose();
            if (_workStarting != null) _workStarting();
            if (_work != null)
            {
                while (true)
                {
                    CheckDispose();
                    lock (_threads)
                    {
                        if (_threads.Count > ThreadAmount || _waitStop)
                        {
                            _logger.WriteInfo(string.Format("当前总开启线程数{0}大于最大线程数{1},当前线程停止", _threads.Count, ThreadAmount));
                            //若大于则当前线程停止
                            break;
                        }
                    }
                    _work();
                    System.Threading.Thread.Sleep(Delay);
                }
                //移除当前线程
                lock (_threads)
                {
                    _logger.WriteInfo(string.Format("当前线程{0}执行完毕，开始移除", System.Threading.Thread.CurrentThread.ManagedThreadId));
                    _threads.RemoveAll(
                        t => t.ManagedThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId);
                    //已不存在处理线程则发送信号量,通知所有线程已停止
                    if (!_threads.Any())
                    {
                        _logger.WriteInfo("线程全部停止");
                        _stopAutoResetEvent.Set();
                        IsStart = false;
                    }
                }
            }
        }
        /// <summary>
        /// 改变线程数
        /// </summary>
        /// <param name="num"></param>
        public void ChangeThreadAmount(int num)
        {
            CheckDispose();
            Interlocked.Exchange(ref _threadAmouont, num);
            AddThread(_threadAmouont - _threads.Count);
            OnThreadAmountChanged(this);
        }
        /// <summary>
        /// 增加线程数
        /// </summary>
        /// <param name="num"></param>
        /// <param name="isStart"></param>
        private void AddThread(int num, bool isStart = true)
        {
            CheckDispose();
            if (num > 0)
            {
                _logger.WriteInfo(string.Format("增加{0}个线程", num));
            }
            lock (_threads)
            {
                for (int i = 0; i < num; i++)
                {
                    System.Threading.Thread thread = new System.Threading.Thread(OnWork) { IsBackground = true };
                    _threads.Add(thread);
                    if (isStart)
                    {
                        thread.Start();
                        IsStart = true;
                    }
                }
            }
        }

        protected virtual void OnThreadAmountChanged(object arg1)
        {
            CheckDispose();
            var handler = _threadAmountChanged;
            if (handler != null) handler(arg1, EventArgs.Empty);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDispose = false;

        private void Dispose(bool isDispose)
        {
            if (!isDispose) return;
            _isDispose = true;
            this.Stop();
            this._work = null;
            this._workStarting = null;
        }

        private void CheckDispose()
        {
            if (_isDispose) throw new ObjectDisposedException("当前对象已释放");
        }
    }
}
