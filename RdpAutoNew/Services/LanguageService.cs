using System.Globalization;

/*
 * Localisation class for top ten languages for user/error messages
 * 
 * Uses windows iso 2 letter language key, defaulting to English. 
 * 
 */
public static class LanguageService
{
    private static readonly string L =
        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

    public static string T(string key)
    {
        switch (L)
        {
            case "es": return Spanish(key);
            case "fr": return French(key);
            case "de": return German(key);
            case "pt": return Portuguese(key);
            case "it": return Italian(key);
            case "ru": return Russian(key);
            case "ja": return Japanese(key);
            case "ko": return Korean(key);
            case "zh": return Chinese(key);
            default: return English(key);
        }
    }

    // ---------------- ENGLISH ----------------
    static string English(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "No Parameters. Missing RDP path\n'-usage' To see parameter usage";

            case "WindowNotFound":
                return "RDP window not found";

            case "CheckboxNotFound":
                return "Checkbox not found: ID/Name:";

            case "NotCheckbox":
                return "Not a checkbox: ID/Name:";

            case "CheckboxNotInteractable":
                return "Checkbox not interactable: ID/Name:";

            case "ButtonNotFound":
                return "Button not found:";

            case "ButtonNotClickable":
                return "Button not clickable: ";

            case "InvalidProcessType":
                return "Error, program has passed an invalid type to ProcessAllAvailableCheckboxes (only 0 or 1 accepted, passed=";

            case "ConnectButtonFailed":
                return "Failed to click Connect button";

            case "Usage":
                return
                    "Usage\n" +
                    "-----\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   This will show this message and explain exe usage\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <Optional Checkbox Names or AutomationIds>'\n" +
                    " E.g 'RdpAutoClick.exe Clipboard Drives 16553'\n" +
                    "   This will click checkboxes named Clipboard and Drives and with Id 16553\n\n"+
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   This will select all checkboxes before connecting\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   This will report back the names and ids of all available Checkboxes";

            default:
                return k;
        }
    }

    // ---------------- SPANISH ----------------
    static string Spanish(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "Sin parámetros. Falta la ruta RDP\n'-usage' para ver el uso";

            case "WindowNotFound":
                return "Ventana RDP no encontrada";

            case "CheckboxNotFound":
                return "Casilla no encontrada: ID/Nombre:";

            case "NotCheckbox":
                return "No es una casilla: ID/Nombre:";

            case "CheckboxNotInteractable":
                return "Casilla no interactuable: ID/Nombre:";

            case "ButtonNotFound":
                return "Botón no encontrado: ";

            case "ButtonNotClickable":
                return "Botón no clicable: ";

            case "InvalidProcessType":
                return "Error, tipo inválido en ProcessAllAvailableCheckboxes (solo se acepta 0 o 1, =";

            case "ConnectButtonFailed":
                return "No se pudo hacer clic en el botón de conexión";

            case "Usage":
                return
                    "Uso\n" +
                    "----\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <Opcional Checkbox Names o AutomationIds>'\n" +
                    "   Ej: 'RdpAutoClick.exe Clipboard Drives 16553'\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   Muestra este mensaje y explica el uso\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   Selecciona todas las casillas antes de conectar\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   Muestra nombres e IDs de todas las casillas disponibles";

            default:
                return English(k);
        }
    }

    // ---------------- FRENCH ----------------
    static string French(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "Aucun paramètre. Chemin RDP manquant\n'-usage' pour voir l'utilisation";

            case "WindowNotFound":
                return "Fenêtre RDP introuvable";

            case "CheckboxNotFound":
                return "Case introuvable: ID/Nom:";

            case "NotCheckbox":
                return "Pas une case à cocher: ID/Nom:";

            case "CheckboxNotInteractable":
                return "Case non interactive: ID/Nom:";

            case "ButtonNotFound":
                return "Bouton introuvable: ";

            case "ButtonNotClickable":
                return "Bouton non cliquable: ";

            case "InvalidProcessType":
                return "Erreur, type invalide passé à ProcessAllAvailableCheckboxes (seulement 0 ou 1 accepté, =";

            case "ConnectButtonFailed":
                return "Impossible de cliquer sur le bouton de connexion";

            case "Usage":
                return
                    "Utilisation\n" +
                    "-----------\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <Checkbox Names ou AutomationIds optionnels>'\n" +
                    "   Ex: 'RdpAutoClick.exe Clipboard Drives 16553'\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   Affiche ce message et explique l'utilisation\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   Sélectionne toutes les cases avant connexion\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   Affiche les noms et IDs de toutes les cases disponibles";

            default:
                return English(k);
        }
    }

    // ---------------- GERMAN ----------------
    static string German(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "Keine Parameter. RDP-Pfad fehlt\n'-usage' für Hilfe";

            case "WindowNotFound":
                return "RDP-Fenster nicht gefunden";

            case "CheckboxNotFound":
                return "Kontrollkästchen nicht gefunden: ID/Name:";

            case "NotCheckbox":
                return "Kein Kontrollkästchen: ID/Name:";

            case "CheckboxNotInteractable":
                return "Kontrollkästchen nicht bedienbar: ID/Name:";

            case "ButtonNotFound":
                return "Schaltfläche nicht gefunden: ";

            case "ButtonNotClickable":
                return "Schaltfläche nicht anklickbar: ";

            case "InvalidProcessType":
                return "Fehler, ungültiger Typ in ProcessAllAvailableCheckboxes (nur 0 oder 1 erlaubt, =";

            case "ConnectButtonFailed":
                return "Verbindungsschaltfläche konnte nicht angeklickt werden";

            case "Usage":
                return
                    "Verwendung\n" +
                    "----------\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <Optionale Checkbox Names oder AutomationIds>'\n" +
                    "   Bsp: 'RdpAutoClick.exe Clipboard Drives 16553'\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   Zeigt diese Meldung und erklärt die Nutzung\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   Aktiviert alle Kontrollkästchen vor Verbindung\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   Zeigt Namen und IDs aller verfügbaren Kontrollkästchen";

            default:
                return English(k);
        }
    }

    // ---------------- PORTUGUESE ----------------
    static string Portuguese(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "Sem parâmetros. Caminho RDP em falta\n'-usage' para ver ajuda";

            case "WindowNotFound":
                return "Janela RDP não encontrada";

            case "CheckboxNotFound":
                return "Caixa não encontrada: ID/Nome:";

            case "NotCheckbox":
                return "Não é uma caixa: ID/Nome:";

            case "CheckboxNotInteractable":
                return "Caixa não interactiva: ID/Nome:";

            case "ButtonNotFound":
                return "Botão não encontrado: ";

            case "ButtonNotClickable":
                return "Botão não clicável: ";

            case "InvalidProcessType":
                return "Erro, tipo inválido em ProcessAllAvailableCheckboxes ( apenas 0 ou 1 aceites=";

            case "ConnectButtonFailed":
                return "Falha ao clicar no botão de ligação";

            case "Usage":
                return
                    "Utilização\n" +
                    "----------\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <Checkbox Names ou AutomationIds opcionais>'\n" +
                    "   Ex: 'RdpAutoClick.exe Clipboard Drives 16553'\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   Mostra esta mensagem e explica utilização\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   Selecciona todas as caixas antes de ligar\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   Mostra nomes e IDs de todas as caixas disponíveis";

            default:
                return English(k);
        }
    }

    // ---------------- ITALIAN ----------------
    static string Italian(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "Nessun parametro. Percorso RDP mancante\n'-usage' per aiuto";

            case "WindowNotFound":
                return "Finestra RDP non trovata";

            case "CheckboxNotFound":
                return "Casella non trovata: ID/Nome:";

            case "NotCheckbox":
                return "Non è una casella: ID/Nome:";

            case "CheckboxNotInteractable":
                return "Casella non utilizzabile: ID/Nome:";

            case "ButtonNotFound":
                return "Pulsante non trovato: ";

            case "ButtonNotClickable":
                return "Pulsante non cliccabile: ";

            case "InvalidProcessType":
                return "Errore, tipo non valido in ProcessAllAvailableCheckboxes (solo 0 o 1 accettati,=";

            case "ConnectButtonFailed":
                return "Impossibile fare clic sul pulsante di connessione";

            case "Usage":
                return
                    "Utilizzo\n" +
                    "--------\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <Checkbox Names o AutomationIds opzionali>'\n" +
                    "   Es: 'RdpAutoClick.exe Clipboard Drives 16553'\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   Mostra questo messaggio e spiega utilizzo\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   Seleziona tutte le caselle prima della connessione\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   Mostra nomi e ID di tutte le caselle disponibili";

            default:
                return English(k);
        }
    }

    // ---------------- RUSSIAN ----------------
    static string Russian(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "Нет параметров. Отсутствует путь RDP\n'-usage' для справки";

            case "WindowNotFound":
                return "Окно RDP не найдено";

            case "CheckboxNotFound":
                return "Флажок не найден: ID/Имя:";

            case "NotCheckbox":
                return "Не флажок: ID/Имя:";

            case "CheckboxNotInteractable":
                return "Флажок недоступен: ID/Имя:";

            case "ButtonNotFound":
                return "Кнопка не найдена: ";

            case "ButtonNotClickable":
                return "Невозможно нажать кнопку: ";

            case "InvalidProcessType":
                return "Ошибка, неверный тип ProcessAllAvailableCheckboxes ( допустимо только 0 или 1,=";

            case "ConnectButtonFailed":
                return "Не удалось нажать кнопку подключения";

            case "Usage":
                return
                    "Использование\n" +
                    "-------------\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <Checkbox Names или AutomationIds>'\n" +
                    "   Пример: 'RdpAutoClick.exe Clipboard Drives 16553'\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   Показать это сообщение и справку\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   Выбрать все флажки перед подключением\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   Показать все доступные флажки";

            default:
                return English(k);
        }
    }

    // ---------------- JAPANESE ----------------
    static string Japanese(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "パラメータなし。RDPパスがありません\n'-usage' で使用方法を表示";

            case "WindowNotFound":
                return "RDPウィンドウが見つかりません";

            case "CheckboxNotFound":
                return "チェックボックスが見つかりません: ID/名前:";

            case "NotCheckbox":
                return "チェックボックスではありません: ID/名前:";

            case "CheckboxNotInteractable":
                return "チェックボックスを操作できません: ID/名前:";

            case "ButtonNotFound":
                return "ボタンが見つかりません: ";

            case "ButtonNotClickable":
                return "ボタンをクリックできません: ";

            case "InvalidProcessType":
                return "エラー、ProcessAllAvailableCheckboxes に無効な型 (、0または1のみ許可,=";

            case "ConnectButtonFailed":
                return "接続ボタンをクリックできませんでした";

            case "Usage":
                return
                    "使用方法\n" +
                    "--------\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <Checkbox Names または AutomationIds>'\n" +
                    "   例: 'RdpAutoClick.exe Clipboard Drives 16553'\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   このメッセージと使用方法を表示\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   接続前にすべてのチェックボックスを選択\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   利用可能なチェックボックス一覧を表示";

            default:
                return English(k);
        }
    }

    // ---------------- KOREAN ----------------
    static string Korean(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "매개변수가 없습니다. RDP 경로 누락\n'-usage' 도움말";

            case "WindowNotFound":
                return "RDP 창을 찾을 수 없습니다";

            case "CheckboxNotFound":
                return "체크박스를 찾을 수 없습니다: ID/이름:";

            case "NotCheckbox":
                return "체크박스가 아닙니다: ID/이름:";

            case "CheckboxNotInteractable":
                return "체크박스를 조작할 수 없습니다: ID/이름:";

            case "ButtonNotFound":
                return "버튼을 찾을 수 없습니다: ";

            case "ButtonNotClickable":
                return "버튼을 클릭할 수 없습니다: ";

            case "InvalidProcessType":
                return "오류, ProcessAllAvailableCheckboxes 잘못된 유형 (, 0 또는 1만 허용,=";

            case "ConnectButtonFailed":
                return "연결 버튼을 클릭하지 못했습니다";

            case "Usage":
                return
                    "사용법\n" +
                    "------\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <Checkbox 이름 또는 AutomationIds>'\n" +
                    "   예: 'RdpAutoClick.exe Clipboard Drives 16553'\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   이 메시지 및 사용법 표시\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   연결 전 모든 체크박스 선택\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   사용 가능한 체크박스 이름 및 ID 표시";

            default:
                return English(k);
        }
    }

    // ---------------- CHINESE (SIMPLIFIED) ----------------
    static string Chinese(string k)
    {
        switch (k)
        {
            case "NoParameters":
                return "没有参数。缺少RDP路径\n使用 '-usage' 查看用法";

            case "WindowNotFound":
                return "未找到RDP窗口";

            case "CheckboxNotFound":
                return "未找到复选框: ID/名称:";

            case "NotCheckbox":
                return "不是复选框: ID/名称:";

            case "CheckboxNotInteractable":
                return "复选框无法操作: ID/名称:";

            case "ButtonNotFound":
                return "未找到按钮: ";

            case "ButtonNotClickable":
                return "按钮无法点击: ";

            case "InvalidProcessType":
                return "错误，ProcessAllAvailableCheckboxes 参数无效 (，仅允许 0 或 1,=";

            case "ConnectButtonFailed":
                return "无法点击连接按钮";

            case "Usage":
                return
                    "用法\n" +
                    "----\n\n" +
                    "'RdpAutoClick.exe <RdpPath> <可选 Checkbox 名称或 AutomationIds>'\n" +
                    "   示例: 'RdpAutoClick.exe Clipboard Drives 16553'\n\n" +
                    "'RdpAutoClick.exe -usage'\n" +
                    "   显示此消息并说明用法\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -all'\n" +
                    "   连接前选择所有复选框\n\n" +
                    "'RdpAutoClick.exe <RdpPath> -showIds'\n" +
                    "   显示所有可用复选框名称和ID";

            default:
                return English(k);
        }
    }
}