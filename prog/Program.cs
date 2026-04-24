namespace ARM_Отдела_кадров
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Глобальная обработка необработанных исключений в потоках
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            // Глобальная обработка необработанных исключений в домене приложения
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show($"Произошла непредвиденная ошибка:\n\n{e.Exception.Message}\n\n" +
                "Рекомендуется перезапустить приложение.\n" +
                "Если ошибка повторяется, обратитесь к разработчику.",
                "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            MessageBox.Show($"Произошла фатальная ошибка:\n\n{ex.Message}\n\n" +
                "Приложение будет закрыто.",
                "Фатальная ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}