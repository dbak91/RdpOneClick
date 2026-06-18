using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;

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
            case "ErrorToggling":
                return "Error toggling ID/Name '";
            case "FoundCheckboxes":
                return "Total found checkboxes";
            case "ErrorClickingButton":
                return "Error clicking button with id=";
            case "DesktopLocation":
                return "Please select a desktop location.";
            case "ExeLocation":
                return "Please select the RdpAutoClick.exe file.";
            case "RdpLocation":
                return "Please select an RDP file.";
            case "DesktopNotExist":
                return "Desktop location does not exist.";
            case "RdpNotExist":
                return "RDP file does not exist.";
            case "ExeNotExist":
                return "RdpAutoClick.exe file does not exist.";
            case "CreatedSuccess":
                return "Shortcut created successfully on the desktop!";
            case "ErrorCreating":
                return "Error creating desktop shortcut";
            case "Ready":
                return "Ready";
            case "Configure":
                return "Please configure...";
            case "SelectAllDesc":
                return "Selects all available options in the popup (without ticking here)";
            case "ShortcutName":
                return "Shortcut Name:";
            case "DesktopLabel":
                return "Desktop Location:";
            case "Browse":
                return "Browse...";
            case "TargetRdp":
                return "Target .RDP File:";
            case "OptionsLabel":
                return "Options To Remember:";
            case "Create":
                return "Create Shortcut";
            case "Loading":
                return "Loading RDP Options...";
            case "RdpAuto":
                return "RdpAutoClick.exe (this):";
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
            case "ErrorToggling":
                return "Error al cambiar el ID/Nombre '";
            case "FoundCheckboxes":
                return "Total de casillas encontradas";
            case "ErrorClickingButton":
                return "Error al hacer clic en el botón con id=";
            case "DesktopLocation":
                return "Seleccione una ubicación para el escritorio.";
            case "ExeLocation":
                return "Seleccione el archivo RdpAutoClick.exe.";
            case "RdpLocation":
                return "Seleccione un archivo RDP.";
            case "DesktopNotExist":
                return "La ubicación del escritorio no existe.";
            case "RdpNotExist":
                return "El archivo RDP no existe.";
            case "ExeNotExist":
                return "El archivo RdpAutoClick.exe no existe.";
            case "CreatedSuccess":
                return "¡Acceso directo creado correctamente en el escritorio!";
            case "ErrorCreating":
                return "Error al crear el acceso directo en el escritorio";
            case "Ready":
                return "Listo";
            case "Configure":
                return "Por favor, configure...";
            case "SelectAllDesc":
                return "Selecciona todas las opciones disponibles en la ventana emergente (sin marcar aquí)";
            case "ShortcutName":
                return "Nombre del acceso directo:";
            case "DesktopLabel":
                return "Ubicación del escritorio:";
            case "Browse":
                return "Examinar...";
            case "TargetRdp":
                return "Archivo .RDP de destino:";
            case "OptionsLabel":
                return "Opciones para recordar:";
            case "Create":
                return "Crear acceso directo";
            case "Loading":
                return "Cargando opciones RDP...";
            case "RdpAuto":
                return "RdpAutoClick.exe (este):";
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
            case "ErrorToggling":
                return "Erreur lors du changement de l'ID/Nom '";
            case "FoundCheckboxes":
                return "Nombre total de cases trouvées";
            case "ErrorClickingButton":
                return "Erreur lors du clic sur le bouton avec id=";
            case "DesktopLocation":
                return "Veuillez sélectionner un emplacement pour le bureau.";
            case "ExeLocation":
                return "Veuillez sélectionner le fichier RdpAutoClick.exe.";
            case "RdpLocation":
                return "Veuillez sélectionner un fichier RDP.";
            case "DesktopNotExist":
                return "L'emplacement du bureau n'existe pas.";
            case "RdpNotExist":
                return "Le fichier RDP n'existe pas.";
            case "ExeNotExist":
                return "Le fichier RdpAutoClick.exe n'existe pas.";
            case "CreatedSuccess":
                return "Raccourci créé avec succès sur le bureau !";
            case "ErrorCreating":
                return "Erreur lors de la création du raccourci sur le bureau";
            case "Ready":
                return "Prêt";
            case "Configure":
                return "Veuillez configurer...";
            case "SelectAllDesc":
                return "Sélectionne toutes les options disponibles dans la fenêtre contextuelle (sans cocher ici)";
            case "ShortcutName":
                return "Nom du raccourci :";
            case "DesktopLabel":
                return "Emplacement du bureau :";
            case "Browse":
                return "Parcourir...";
            case "TargetRdp":
                return "Fichier .RDP cible :";
            case "OptionsLabel":
                return "Options à mémoriser :";
            case "Create":
                return "Créer un raccourci";
            case "Loading":
                return "Chargement des options RDP...";
            case "RdpAuto":
                return "RdpAutoClick.exe (celui-ci) :";
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
            case "ErrorToggling":
                return "Fehler beim Umschalten der ID/des Namens '";
            case "FoundCheckboxes":
                return "Gefundene Kontrollkästchen insgesamt";
            case "ErrorClickingButton":
                return "Fehler beim Klicken auf die Schaltfläche mit id=";
            case "DesktopLocation":
                return "Bitte wählen Sie einen Desktop-Speicherort aus.";
            case "ExeLocation":
                return "Bitte wählen Sie die Datei RdpAutoClick.exe aus.";
            case "RdpLocation":
                return "Bitte wählen Sie eine RDP-Datei aus.";
            case "DesktopNotExist":
                return "Der Desktop-Speicherort existiert nicht.";
            case "RdpNotExist":
                return "Die RDP-Datei existiert nicht.";
            case "ExeNotExist":
                return "Die Datei RdpAutoClick.exe existiert nicht.";
            case "CreatedSuccess":
                return "Verknüpfung erfolgreich auf dem Desktop erstellt!";
            case "ErrorCreating":
                return "Fehler beim Erstellen der Desktop-Verknüpfung";
            case "Ready":
                return "Bereit";
            case "Configure":
                return "Bitte konfigurieren...";
            case "SelectAllDesc":
                return "Wählt alle verfügbaren Optionen im Popup aus (ohne hier anzukreuzen)";
            case "ShortcutName":
                return "Name der Verknüpfung:";
            case "DesktopLabel":
                return "Desktop-Speicherort:";
            case "Browse":
                return "Durchsuchen...";
            case "TargetRdp":
                return "Ziel-.RDP-Datei:";
            case "OptionsLabel":
                return "Zu speichernde Optionen:";
            case "Create":
                return "Verknüpfung erstellen";
            case "Loading":
                return "RDP-Optionen werden geladen...";
            case "RdpAuto":
                return "RdpAutoClick.exe (dieses):";
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
            case "ErrorToggling":
                return "Erro ao alternar ID/Nome '";
            case "FoundCheckboxes":
                return "Total de caixas de seleção encontradas";
            case "ErrorClickingButton":
                return "Erro ao clicar no botão com id=";
            case "DesktopLocation":
                return "Selecione uma localização para o ambiente de trabalho.";
            case "ExeLocation":
                return "Selecione o ficheiro RdpAutoClick.exe.";
            case "RdpLocation":
                return "Selecione um ficheiro RDP.";
            case "DesktopNotExist":
                return "A localização do ambiente de trabalho não existe.";
            case "RdpNotExist":
                return "O ficheiro RDP não existe.";
            case "ExeNotExist":
                return "O ficheiro RdpAutoClick.exe não existe.";
            case "CreatedSuccess":
                return "Atalho criado com sucesso no ambiente de trabalho!";
            case "ErrorCreating":
                return "Erro ao criar atalho no ambiente de trabalho";
            case "Ready":
                return "Pronto";
            case "Configure":
                return "Configure...";
            case "SelectAllDesc":
                return "Seleciona todas as opções disponíveis na janela (sem marcar aqui)";
            case "ShortcutName":
                return "Nome do atalho:";
            case "DesktopLabel":
                return "Localização do ambiente de trabalho:";
            case "Browse":
                return "Procurar...";
            case "TargetRdp":
                return "Ficheiro .RDP de destino:";
            case "OptionsLabel":
                return "Opções a memorizar:";
            case "Create":
                return "Criar atalho";
            case "Loading":
                return "A carregar opções RDP...";
            case "RdpAuto":
                return "RdpAutoClick.exe (este):";
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
            case "ErrorToggling":
                return "Errore durante la modifica dell'ID/Nome '";
            case "FoundCheckboxes":
                return "Numero totale di caselle trovate";
            case "ErrorClickingButton":
                return "Errore durante il clic sul pulsante con id=";
            case "DesktopLocation":
                return "Selezionare una posizione del desktop.";
            case "ExeLocation":
                return "Selezionare il file RdpAutoClick.exe.";
            case "RdpLocation":
                return "Selezionare un file RDP.";
            case "DesktopNotExist":
                return "La posizione del desktop non esiste.";
            case "RdpNotExist":
                return "Il file RDP non esiste.";
            case "ExeNotExist":
                return "Il file RdpAutoClick.exe non esiste.";
            case "CreatedSuccess":
                return "Collegamento creato con successo sul desktop!";
            case "ErrorCreating":
                return "Errore durante la creazione del collegamento sul desktop";
            case "Ready":
                return "Pronto";
            case "Configure":
                return "Configurare...";
            case "SelectAllDesc":
                return "Seleziona tutte le opzioni disponibili nella finestra popup (senza spuntare qui)";
            case "ShortcutName":
                return "Nome del collegamento:";
            case "DesktopLabel":
                return "Posizione del desktop:";
            case "Browse":
                return "Sfoglia...";
            case "TargetRdp":
                return "File .RDP di destinazione:";
            case "OptionsLabel":
                return "Opzioni da ricordare:";
            case "Create":
                return "Crea collegamento";
            case "Loading":
                return "Caricamento opzioni RDP...";
            case "RdpAuto":
                return "RdpAutoClick.exe (questo):";
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
            case "ErrorToggling":
                return "Ошибка при переключении ID/Имени '";
            case "FoundCheckboxes":
                return "Всего найдено флажков";
            case "ErrorClickingButton":
                return "Ошибка при нажатии кнопки с id=";
            case "DesktopLocation":
                return "Пожалуйста, выберите расположение рабочего стола.";
            case "ExeLocation":
                return "Пожалуйста, выберите файл RdpAutoClick.exe.";
            case "RdpLocation":
                return "Пожалуйста, выберите файл RDP.";
            case "DesktopNotExist":
                return "Расположение рабочего стола не существует.";
            case "RdpNotExist":
                return "Файл RDP не существует.";
            case "ExeNotExist":
                return "Файл RdpAutoClick.exe не существует.";
            case "CreatedSuccess":
                return "Ярлык успешно создан на рабочем столе!";
            case "ErrorCreating":
                return "Ошибка при создании ярлыка на рабочем столе";
            case "Ready":
                return "Готово";
            case "Configure":
                return "Пожалуйста, настройте...";
            case "SelectAllDesc":
                return "Выбирает все доступные параметры во всплывающем окне (без отметки здесь)";
            case "ShortcutName":
                return "Имя ярлыка:";
            case "DesktopLabel":
                return "Расположение рабочего стола:";
            case "Browse":
                return "Обзор...";
            case "TargetRdp":
                return "Целевой файл .RDP:";
            case "OptionsLabel":
                return "Параметры для сохранения:";
            case "Create":
                return "Создать ярлык";
            case "Loading":
                return "Загрузка параметров RDP...";
            case "RdpAuto":
                return "RdpAutoClick.exe (этот):";
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
            case "ErrorToggling":
                return "ID/名前の切り替え中にエラーが発生しました '";
            case "FoundCheckboxes":
                return "見つかったチェックボックス数";
            case "ErrorClickingButton":
                return "id のボタンのクリック中にエラーが発生しました=";
            case "DesktopLocation":
                return "デスクトップの場所を選択してください。";
            case "ExeLocation":
                return "RdpAutoClick.exe ファイルを選択してください。";
            case "RdpLocation":
                return "RDP ファイルを選択してください。";
            case "DesktopNotExist":
                return "デスクトップの場所が存在しません。";
            case "RdpNotExist":
                return "RDP ファイルが存在しません。";
            case "ExeNotExist":
                return "RdpAutoClick.exe ファイルが存在しません。";
            case "CreatedSuccess":
                return "ショートカットがデスクトップに正常に作成されました。";
            case "ErrorCreating":
                return "デスクトップショートカットの作成中にエラーが発生しました";
            case "Ready":
                return "準備完了";
            case "Configure":
                return "設定してください...";
            case "SelectAllDesc":
                return "ポップアップ内のすべての利用可能なオプションを選択します（ここではチェックしません）";
            case "ShortcutName":
                return "ショートカット名:";
            case "DesktopLabel":
                return "デスクトップの場所:";
            case "Browse":
                return "参照...";
            case "TargetRdp":
                return "対象 .RDP ファイル:";
            case "OptionsLabel":
                return "記憶するオプション:";
            case "Create":
                return "ショートカットを作成";
            case "Loading":
                return "RDP オプションを読み込み中...";
            case "RdpAuto":
                return "RdpAutoClick.exe（これ）:";
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
            case "ErrorToggling":
                return "ID/이름 전환 중 오류 발생 '";
            case "FoundCheckboxes":
                return "찾은 체크박스 수";
            case "ErrorClickingButton":
                return "id 버튼 클릭 중 오류 발생=";
            case "DesktopLocation":
                return "바탕 화면 위치를 선택하세요.";
            case "ExeLocation":
                return "RdpAutoClick.exe 파일을 선택하세요.";
            case "RdpLocation":
                return "RDP 파일을 선택하세요.";
            case "DesktopNotExist":
                return "바탕 화면 위치가 존재하지 않습니다.";
            case "RdpNotExist":
                return "RDP 파일이 존재하지 않습니다.";
            case "ExeNotExist":
                return "RdpAutoClick.exe 파일이 존재하지 않습니다.";
            case "CreatedSuccess":
                return "바탕 화면에 바로 가기가 성공적으로 생성되었습니다!";
            case "ErrorCreating":
                return "바탕 화면 바로 가기 생성 오류";
            case "Ready":
                return "준비 완료";
            case "Configure":
                return "설정해 주세요...";
            case "SelectAllDesc":
                return "팝업의 모든 사용 가능한 옵션을 선택합니다(여기서는 체크하지 않음)";
            case "ShortcutName":
                return "바로 가기 이름:";
            case "DesktopLabel":
                return "바탕 화면 위치:";
            case "Browse":
                return "찾아보기...";
            case "TargetRdp":
                return "대상 .RDP 파일:";
            case "OptionsLabel":
                return "기억할 옵션:";
            case "Create":
                return "바로 가기 만들기";
            case "Loading":
                return "RDP 옵션 불러오는 중...";
            case "RdpAuto":
                return "RdpAutoClick.exe (이것):";
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
            case "ErrorToggling":
                return "切换 ID/名称时出错 '";
            case "FoundCheckboxes":
                return "找到的复选框总数";
            case "ErrorClickingButton":
                return "点击 id 按钮时出错=";
            case "DesktopLocation":
                return "请选择桌面位置。";
            case "ExeLocation":
                return "请选择 RdpAutoClick.exe 文件。";
            case "RdpLocation":
                return "请选择 RDP 文件。";
            case "DesktopNotExist":
                return "桌面位置不存在。";
            case "RdpNotExist":
                return "RDP 文件不存在。";
            case "ExeNotExist":
                return "RdpAutoClick.exe 文件不存在。";
            case "CreatedSuccess":
                return "快捷方式已成功创建到桌面！";
            case "ErrorCreating":
                return "创建桌面快捷方式时出错";
            case "Ready":
                return "就绪";
            case "Configure":
                return "请进行配置...";
            case "SelectAllDesc":
                return "选择弹出窗口中的所有可用选项（不在此处勾选）";
            case "ShortcutName":
                return "快捷方式名称:";
            case "DesktopLabel":
                return "桌面位置:";
            case "Browse":
                return "浏览...";
            case "TargetRdp":
                return "目标 .RDP 文件:";
            case "OptionsLabel":
                return "要记住的选项:";
            case "Create":
                return "创建快捷方式";
            case "Loading":
                return "正在加载 RDP 选项...";
            case "RdpAuto":
                return "RdpAutoClick.exe（此程序）:";
            default:
                return English(k);
        }
    }
}