public static class Help
{
    public static string CommandHandler()
    {
        return "Я \\- бот распознающий хокку и танка во введенных пользователями в чатах (или написанных лично мне) сообщениях." +
        "Обычно я молчу, но если я увижу сообщение, начало которого можно переформотировать в свойственный хокку формат \"трёхстрочного 5-75 стихотворения\", " +
        "то я это непременно отмечу. Больше о поэззии [хокку](https://ru.wikipedia.org/wiki/%D0%A5%D0%B0%D0%B9%D0%BA%D1%83)" +
        "или [танка](https://ru.wikipedia.org/wiki/%D0%A2%D0%B0%D0%BD%D0%BA%D0%B0) можно узнать кликнув по ссылкам. " +
        Environment.NewLine + Environment.NewLine +
        "Ах да - я работаю только на русском языке. На других, к сожалению, не планирую";
    }
}