public class PrefixService
{
    private static readonly string[] YerAPoetHarry = new string[]{
        "Да ты поэт!",
        "Кто-то увлекался японской культурой",
        "Надо же, нашлось",
        "Сёгун - он везде сёгун",
        "Прямо по Фрейду, в этом сообщении точно есть подсознательное хокку, сейчас покажу:",
        "Ты самурай, Гарри:",
        "いいね！",
        "素晴らしい！"
    };

    private bool PrefixExists;


    public PrefixService()
    {
        PrefixExists = false;
    }

    public string TryAddPrefix()
    {
        if (PrefixExists)
            return string.Empty;

        PrefixExists = true;

        return RandomHelper.RandomString(YerAPoetHarry) + Environment.NewLine + Environment.NewLine;
    }
}