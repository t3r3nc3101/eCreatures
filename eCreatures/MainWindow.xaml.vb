
Imports System.Runtime.InteropServices
Imports System.Windows.Threading
Imports System.Windows
Imports System.Windows.Forms
Imports DotNetBrowser.WPF
Imports System.Windows.Input
Imports Microsoft.Windows.forms



Class MainWindow

    Dim followMouse = False
    Dim stayStill = False
    Dim gravity = True
    Dim directionAmount = 0
    Dim mouseLocation As Point


    Dim display1Width As Integer = System.Windows.SystemParameters.WorkArea.Width
    Dim display1Height As Integer = System.Windows.SystemParameters.WorkArea.Height

    Function moveDown()
        Dim currentTopPosition = Me.Top
        If currentTopPosition < display1Height - 250 And gravity = True Then
            Me.Top = currentTopPosition + 1
            'Threading.Thread.Sleep(100)
            'Debug.WriteLine(currentTopPosition)
            moveDown()
        ElseIf currentTopPosition > display1Height - 250 Then
            moveUp()
        End If
    End Function
    Function moveUp()
        Dim currentTopPosition = Me.Top
        If currentTopPosition > display1Height - 250 Then
            Me.Top = currentTopPosition - 1
            moveUp()
        End If
        If currentTopPosition < display1Height - 250 Then
            moveDown()
        End If
    End Function


    ' tstzzzzz

    Function gravityChecked()
        Debug.WriteLine("Gravity Enabled!")
        gravity = True
        moveDown()
    End Function
    Function gravityUnchecked()
        Debug.WriteLine("Gravity Disabled!")
        gravity = False
    End Function

    Function stayStillChecked()
        Debug.WriteLine("Stay still Enabled!")
        stayStill = True

    End Function
    Function stayStillUnchecked()
        Debug.WriteLine("Stay still Disabled!")
        stayStill = False
    End Function

    ' tstzzzzz



    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Me.WindowStyle = 0

        moveDown()

        Debug.WriteLine(display1Width.ToString)
        Debug.WriteLine(display1Height.ToString)


        Dim generator As New Random()
        Dim randomvalue As Integer
        randomvalue = generator.Next(1, display1Width)
        ' Debug.WriteLine(" " + randomvalue.ToString())

        Me.Left = randomvalue.ToString()

        NewTimerPls()


    End Sub

    Private Sub Window_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        moveDown()
    End Sub

    Private Sub Window_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        Me.DragMove()
    End Sub


    Dim currentCreature = 0

    Function changeimage()

        ' getRunningProgs()

        Dim imageRef = New BitmapImage


        If currentCreature = 0 Then
            imageRef.BeginInit()
            imageRef.UriSource = New Uri("pack://application:,,,/char.gif")
            imageRef.EndInit()
            currentCreature = 1
        ElseIf currentCreature = 1 Then
            imageRef.BeginInit()
            imageRef.UriSource = New Uri("pack://application:,,,/float.gif")
            imageRef.EndInit()
            currentCreature = 2
        ElseIf currentCreature = 2 Then
            imageRef.BeginInit()
            imageRef.UriSource = New Uri("pack://application:,,,/dragon.gif")
            imageRef.EndInit()
            currentCreature = 3
        ElseIf currentCreature = 3 Then
            imageRef.BeginInit()
            imageRef.UriSource = New Uri("pack://application:,,,/shroom.gif")
            imageRef.EndInit()
            currentCreature = 4

        ElseIf currentCreature = 4 Then
            imageRef.BeginInit()
            imageRef.UriSource = New Uri("pack://application:,,,/pika.gif")
            imageRef.EndInit()
            currentCreature = 0
        End If

        WpfAnimatedGif.ImageBehavior.SetAnimatedSource(image, imageRef)

    End Function

    Dim testid = 0

    Function getRunningProgs()

        For Each proc As Process In Process.GetProcesses
            If proc.MainWindowTitle <> "" Then
                'proc.
                Debug.WriteLine(proc.ProcessName)
                testid = proc.Id
            End If
        Next


    End Function





    ''''' TIMER

    Private dpTimer As DispatcherTimer

    Public Sub NewTimerPls()
        dpTimer = New DispatcherTimer
        dpTimer.Interval = TimeSpan.FromMilliseconds(1) '5000
        AddHandler dpTimer.Tick, AddressOf TickMe
        dpTimer.Start()
        NewDirTimer()
        NewTimerPls3()
    End Sub

    Private Sub TickMe()
        'Do whatever here 
        'Debug.WriteLine(directionAmount)
        ' dpTimer.Stop()

        Dim mouseIsDown As Boolean = System.Windows.Input.Mouse.LeftButton = MouseButtonState.Pressed
        If mouseIsDown = False And contextOpenBoi = False Then
            Dim currentLeftPosition = Me.Left
            ' Debug.WriteLine(currentLeftPosition)                                           '<<<<<<---- DEBUG LEFT POS
            Me.Left = currentLeftPosition + directionAmount
        End If

    End Sub



    ''''' TIMER2222222222222

    Private dpTimer2 As DispatcherTimer
    Public Sub NewDirTimer()
        dpTimer2 = New DispatcherTimer
        dpTimer2.Interval = TimeSpan.FromMilliseconds(200) '5000
        AddHandler dpTimer2.Tick, AddressOf TickMe2
        dpTimer2.Start()
    End Sub

    Private Sub TickMe2()

        Dim generatorz As New Random()
        Dim randomvaluez As Decimal
        randomvaluez = generatorz.Next(200, 1500)
        dpTimer2.Interval = TimeSpan.FromMilliseconds(randomvaluez) '5000

        Dim currentLeftPosition = Me.Left

        If followMouse = False Then
            'Debug.WriteLine(followMouse)
            If currentLeftPosition > 0 And currentLeftPosition < display1Width - 250 And stayStill = False Then     '''' specified image size here
                Dim generator As New Random()
                Dim randomvalue As Decimal
                randomvalue = generator.Next(-2, 3)
                directionAmount = randomvalue
            ElseIf currentLeftPosition <= 0 And stayStill = False Then
                Dim generator As New Random()
                Dim randomvalue As Decimal
                randomvalue = generator.Next(3, 5)
                directionAmount = randomvalue
            ElseIf currentLeftPosition > display1Width - 250 And stayStill = False Then '''' specified image size here
                directionAmount = -5
            End If
        ElseIf followMouse = True Then  '''' folooow moouseee hereeee

            Dim generatorx As New Random()
            Dim randomvaluex As Decimal
            randomvaluex = generatorx.Next(50, 100)
            dpTimer2.Interval = TimeSpan.FromMilliseconds(randomvaluex) '5000

            Debug.WriteLine(mouseLocation.X)
            If mouseLocation.X >= 125 And stayStill = False Then     '''' specified image size here (125 - half image)
                directionAmount = mouseLocation.X * 0.01 ' was 5
            End If
            If mouseLocation.X < 0 And stayStill = False Then     '''' specified image size here (125 - half image)
                Dim tempAmount = mouseLocation.X * 0.01
                directionAmount = tempAmount
            End If
        End If

        If stayStill = True Then
            directionAmount = 0
        End If



    End Sub


    ''''' TIMER 333 for cursorFollow

    Private dpTimer3 As DispatcherTimer

    Public Sub NewTimerPls3()
        dpTimer3 = New DispatcherTimer
        dpTimer3.Interval = TimeSpan.FromMilliseconds(500) '5000
        AddHandler dpTimer3.Tick, AddressOf TickMe3
        dpTimer3.Start()
    End Sub

    Dim contextOpenBoi = False
    Private Sub TickMe3()
        'Do whatever here 
        ' dpTimer.Stop()

        Dim mouseIsDown As Boolean = System.Windows.Input.Mouse.LeftButton = MouseButtonState.Pressed

        If contextOpenBoi = False And mouseIsDown = False Then
            Mouse.Capture(Me)
            mouseLocation = Mouse.GetPosition(Me)
            ReleaseMouseCapture()

            'Debug.WriteLine(mouseLocation)  ''' BEDUGE MOSUE LCAOTOIUN
        End If

    End Sub

    Function kill()
        System.Windows.Application.Current.Shutdown()
    End Function

    Private Sub Window_ContextMenuOpening(sender As Object, e As ContextMenuEventArgs)
        contextOpenBoi = True
    End Sub

    Private Sub Window_ContextMenuClosing(sender As Object, e As ContextMenuEventArgs)
        contextOpenBoi = False
    End Sub


    Private Sub followMouseChecked(sender As Object, e As RoutedEventArgs)
        followMouse = True
        Debug.WriteLine("follow!")
    End Sub
    Private Sub followMouseUnchecked(sender As Object, e As RoutedEventArgs)
        followMouse = False
    End Sub

End Class


