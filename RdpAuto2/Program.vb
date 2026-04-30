Imports System.Runtime.InteropServices
Imports System.Threading


Module RdpAutoClick

    <DllImport("user32.dll", SetLastError:=True)>
    Private Sub SetCursorPos(X As Integer, Y As Integer)
    End Sub

    <DllImport("user32.dll")>
    Private Sub mouse_event(dwFlags As Integer, dx As Integer, dy As Integer, dwData As Integer, dwExtraInfo As Integer)
    End Sub

    <DllImport("user32.dll", SetLastError:=True)>
    Private Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Function MessageBox(hWnd As IntPtr, text As String, caption As String, type As UInteger) As Integer
    End Function

    Function WaitForWindow(title As String, timeoutMs As Integer) As Boolean

        Dim elapsed As Integer = 0
        Dim interval As Integer = 100

        While elapsed < timeoutMs
            Dim hWnd As IntPtr = FindWindow(Nothing, title)

            If hWnd <> IntPtr.Zero Then
                Return True
            End If

            Thread.Sleep(interval)
            elapsed += interval
        End While

        Return False
    End Function

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
                MessageBox(IntPtr.Zero, "Invalid RDP click coordinates.", "RDP AutoClick", 0)
                Return
            End If

            If Not Integer.TryParse(args(3), connectX) OrElse Not Integer.TryParse(args(4), connectY) Then
                MessageBox(IntPtr.Zero, "Invalid connect coordinates.", "RDP AutoClick", 0)
                Return
            End If

            ' Double-click the .rdp file
            ClickAt(rdpX, rdpY)
            Thread.Sleep(100)
            ClickAt(rdpX, rdpY)

            If Not WaitForWindow("Remote Desktop Connection security warning", 5000) Then
                MessageBox(IntPtr.Zero, "RDP window did not appear.", "RDP AutoClick", 0)
                Return
            End If

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
                MessageBox(IntPtr.Zero, "Invalid connect coordinates.", "RDP AutoClick", 0)
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
                MessageBox(IntPtr.Zero, "Failed to start mstsc: " & ex.Message, "RDP AutoClick", 0)
                Return
            End Try

            ' Wait for popup

            If Not WaitForWindow("Remote Desktop Connection security warning", 5000) Then
                MessageBox(IntPtr.Zero, "RDP window did not appear.", "RDP AutoClick", 0)
                Return
            End If
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
                MessageBox(IntPtr.Zero, "Invalid pre-click coordinates at position " & i, "RDP AutoClick", 0)
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
        MessageBox(IntPtr.Zero, "Usage:" + Environment.NewLine +
        "Click mode:" + Environment.NewLine +
        "  RdpAutoClick.exe click <rdpX> <rdpY> <connectX> <connectY> [preX preY]..." + Environment.NewLine +
        "Path mode:" + Environment.NewLine +
        "  RdpAutoClick.exe <rdpPath> <connectX> <connectY> [preX preY]...", "RDP AutoClick", 0)
    End Sub

End Module