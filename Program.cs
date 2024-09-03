using System;
using System.Threading;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{

    /// <summary>
    /// 1) Все асинхронные операции должны содержать в названии Async
    /// 2) MessageBox не лучший варик для обработки ошибок, лучше все заварачивать в свои exception'ы и отправлять выше
    /// 3) Ознакомься в с принципами декомопзиции кода MVVM, FSD и Чистой луковой архитектурой (по сути разбиение на микросервисы)
    /// 4) Best Praties не рекомендуют использовать async void, однако async TASK будет ожидаться event'ом, поэтому как по мне лучше всю логику вынести в сервисы а их в свою очередь сделать асихнронными.
    ///     Но не исплюзуй ASYNC VOID
    /// 5) Что за хуйня с TASK.Delay в лупе?
    /// </summary>
    internal static class Program
    {
        private static Mutex _mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}");
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ///Нахуя тут мьютекс, мьютекс должен быть в try catch и тут он вовсе не нужен
            // Проверяем, что приложение уже не запущено
            if (!_mutex.WaitOne(0, false))
            {
                MessageBox.Show("Приложение уже запущено!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Environment.OSVersion.Version.Major >= 6)
                SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main());

            // Освобождаем мьютекс при закрытии приложения
            _mutex.ReleaseMutex();
        }
    }
}
