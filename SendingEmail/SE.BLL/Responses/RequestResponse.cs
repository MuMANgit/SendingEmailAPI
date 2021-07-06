namespace SE.BLL.Responses
{
    /// <summary>
    /// Класс описыващий ответ запроса
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RequestResponse<T> where T : class
    {
        /// <summary>
        /// Результат выполнения
        /// </summary>
        public T Result { get; set; }
        /// <summary>
        /// Сообщение об ошибке, в случае некоректного запроса
        /// или необработанного исключения
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Флаг
        /// </summary>
        public bool IsSuccess { get; set; } = true;
        /// <summary>
        /// Присваивает свойству Result
        /// результат запроса
        /// </summary>
        /// <param name="model"></param>
        public void SuccessResult(T model)
        {
            Result = model;
        }
        /// <summary>
        /// Присваивает свойству ErrorMessage
        /// информацию об ошибке
        /// </summary>
        /// <param name="message"></param>
        public void ErrorResult(string message)
        {
            IsSuccess = false;
            ErrorMessage = message;
        }
    }
}
