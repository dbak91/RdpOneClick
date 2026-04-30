Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Diagnostics

Module RdpAutoClick

    <DllImport("user32.dll", SetLastError:=True)>
    Private Sub SetCursorPos(X As Integer, Y As Integer)
    End Sub

    <DllImport("user32.dll")>
    Private Sub mouse_event(dwFlags As Integer, dx As Integer, dy As Integer, dwData As Integer, dwExtraInfo As Integer)
    End Sub

    Private Const MOUSEEVENTF_LEFTDOWN As Integer = &H2
    Private Const MOUSEEVENTF_LEFTUP As Integer = &H4

    Sub ClickAt(x As Integer, y As Integer)
        SetCursorPos(x, y)
        Thread.Sleep(50)
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
    End Sub

    Sub Main(args As String())

        If args.Length = 0 Then
            ShowUsage()
            Return
        End If

        Dim connectX As Integer = 0
        Dim connectY As Integer = 0
        Dim startIndex As Integer = 0

        ' -------------------------------
        ' CLICK MODE
        ' -------------------------------
        If args(0).ToLower() = "click" Then

            If args.Length < 5 Then
                ShowUsage()
                Return
            End If

            Dim rdpX As Integer
            Dim rdpY As Integer

            If Not Integer.TryParse(args(1), rdpX) OrElse Not Integer.TryParse(args(2), rdpY) Then
                Console.WriteLine("Invalid RDP click coordinates.")
                Return
            End If

            If Not Integer.TryParse(args(3), connectX) OrElse Not Integer.TryParse(args(4), connectY) Then
                Console.WriteLine("Invalid connect coordinates.")
                Return
            End If

            ' Double-click the .rdp file
            ClickAt(rdpX, rdpY)
            Thread.Sleep(100)
            ClickAt(rdpX, rdpY)

            ' Wait for popup (double tap)
            Thread.Sleep(700)
            Thread.Sleep(700)

            startIndex = 5

        Else
            ' -------------------------------
            ' PATH MODE
            ' -------------------------------

            If args.Length < 3 Then
                ShowUsage()
                Return
            End If

            Dim rdpPath As String = args(0)

            If Not Integer.TryParse(args(1), connectX) OrElse Not Integer.TryParse(args(2), connectY) Then
                Console.WriteLine("Invalid connect coordinates.")
                Return
            End If

            Try
                ' Ensure no cmd window
                Dim psi As New ProcessStartInfo()
                psi.FileName = "mstsc.exe"
                psi.Arguments = """" & rdpPath & """"
                psi.UseShellExecute = False
                psi.CreateNoWindow = True

                Process.Start(psi)
            Catch ex As Exception
                Console.WriteLine("Failed to start mstsc: " & ex.Message)
                Return
            End Try

            ' Wait for popup
            Thread.Sleep(1500)

            startIndex = 3
        End If

        ' -------------------------------
        ' OPTIONAL PRE-CLICKS
        ' -------------------------------
        Dim i As Integer = startIndex

        While i + 1 < args.Length

            Dim preX As Integer
            Dim preY As Integer

            If Not Integer.TryParse(args(i), preX) OrElse Not Integer.TryParse(args(i + 1), preY) Then
                Console.WriteLine("Invalid pre-click coordinates at position " & i)
                Return
            End If

            ClickAt(preX, preY)
            Thread.Sleep(100)

            i += 2
        End While

        ' -------------------------------
        ' FINAL CONNECT CLICK
        ' -------------------------------
        ClickAt(connectX, connectY)

    End Sub

    Sub ShowUsage()
        Console.WriteLine("Usage:")
        Console.WriteLine("")
        Console.WriteLine("Click mode:")
        Console.WriteLine("  RdpAutoClick.exe click <rdpX> <rdpY> <connectX> <connectY> [preX preY]...")
        Console.WriteLine("")
        Console.WriteLine("Path mode:")
        Console.WriteLine("  RdpAutoClick.exe <rdpPath> <connectX> <connectY> [preX preY]...")
    End Sub

End Module