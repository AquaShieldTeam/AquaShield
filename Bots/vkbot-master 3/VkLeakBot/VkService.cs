using System.Text.Json;

public class VkService
{
    private readonly string _token = "vk1.a.lCH5o_lW7LJEsHLT95gPZqx4hgHiPQD9luP5bz0AUGwRvqLFPdewo8yEDLd3-y65VBQ52FJ4q3M8lrkL60oMFK6iuV7TqzX-e8hpo7wcvTV3I__0TddR7rUbsp29oend7FJNfO_TnR0ySVL_iiIsg4K3lnklxcJdZxe424AgbL2eirUhsIT2e1nPMRusJbjYsjJpIonbGMX_-rvoaKea2Q";
    private readonly HttpClient _http = new();

    public string GetMainKeyboard() => @"{""one_time"":false,""buttons"":[[{""action"":{""type"":""text"",""label"":""➕ Добавить"",""payload"":""{\""command\"":\""add\""}""},""color"":""positive""}],[{""action"":{""type"":""text"",""label"":""⚙️ Настройки"",""payload"":""{\""command\"":\""settings\""}""},""color"":""primary""}]]}";

    public string GetAddMenu() => @"{""one_time"":false,""buttons"":[[{""action"":{""type"":""text"",""label"":""Хаб"",""payload"":""{\""command\"":\""hub\""}""},""color"":""default""}],[{""action"":{""type"":""text"",""label"":""Датчик"",""payload"":""{\""command\"":\""sensor\""}""},""color"":""default""}]]}";

    public string GetSettingsMenu() => @"{""one_time"":false,""buttons"":[[{""action"":{""type"":""text"",""label"":""Уведомления"",""payload"":""{\""command\"":\""alert\""}""},""color"":""default""}],[{""action"":{""type"":""text"",""label"":""Режим перекрытия"",""payload"":""{\""command\"":\""overlap\""}""},""color"":""default""}],[{""action"":{""type"":""text"",""label"":""Место размещения"",""payload"":""{\""command\"":\""location\""}""},""color"":""default""}]]}";

    public string GetAlertMenu() => @"{""one_time"":false,""buttons"":[[{""action"":{""type"":""text"",""label"":""Только уведомления в чат"",""payload"":""{\""command\"":\""just_chat\""}""},""color"":""default""}],[{""action"":{""type"":""text"",""label"":""Только звуковое уведомление"",""payload"":""{\""command\"":\""just_sound\""}""},""color"":""default""}],[{""action"":{""type"":""text"",""label"":""Оба уведомления"",""payload"":""{\""command\"":\""both\""}""},""color"":""default""}]]}";

    public string GetOverlapMenu() => @"{""one_time"":false,""buttons"":[[{""action"":{""type"":""text"",""label"":""Да"",""payload"":""{\""command\"":\""overlap_on\""}""},""color"":""positive""},{""action"":{""type"":""text"",""label"":""Нет"",""payload"":""{\""command\"":\""overlap_off\""}""},""color"":""negative""}]]}";

    public string GetBatteryThresholdMenu() => @"{""one_time"":false,""buttons"":[[{""action"":{""type"":""text"",""label"":""20%"",""payload"":""{\""command\"":\""fifth_part\""}""},""color"":""default""},{""action"":{""type"":""text"",""label"":""50%"",""payload"":""{\""command\"":\""half\""}""},""color"":""default""}]]}";

    public string GetConfirmMenu() => @"{""one_time"":false,""buttons"":[[{""action"":{""type"":""text"",""label"":""✅ Подтвердить"",""payload"":""{\""command\"":\""confirm\""}""},""color"":""positive""},{""action"":{""type"":""text"",""label"":""❌ Отмена"",""payload"":""{\""command\"":\""cancellation\""}""},""color"":""negative""}]]}";

    public async Task SendMessageWithKeyboard(long peerId, string text, string keyboardJson)
    {
        var url = $"https://api.vk.com/method/messages.send?v=5.199" +
                  $"&access_token={_token}" +
                  $"&peer_id={peerId}" +
                  $"&message={Uri.EscapeDataString(text)}" +
                  $"&keyboard={Uri.EscapeDataString(keyboardJson)}" +
                  $"&random_id={Random.Shared.Next(1000000, 9999999)}";

        await _http.GetStringAsync(url);
    }
}