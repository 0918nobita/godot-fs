namespace MyGameFs

open Godot

type ButtonFs() =
    inherit Button()

    let mutable count = 0

    override this._Ready() =
        GD.Print("Hello from F#!")
        this.GetNode(new NodePath("/root/Main"))
            .Connect("CustomSignal", this, "_OnSignalReceived")
        |> ignore

    member this._OnSignalReceived(n: int) =
        GD.Print(sprintf "Received: %d" n)

    member this._OnButtonPressed() =
        let label = this.GetNode<Label>(new NodePath("/root/Main/Label"))
        label.Text <-
            if count > 0
            then sprintf "PRESSED! (%d)" count
            else "PRESSED!"
        count <- count + 1

type Direction = ToLeft | ToRight

type IconFs() =
    inherit Sprite()

    let mutable elapsedTime = 0.0

    override this._PhysicsProcess(delta: float32) =
        let (|Vector2|) (vec: Vector2) = (vec.x, vec.y)

        let (|Odd|Even|) (n: int) = if n % 2 = 0 then Even else Odd

        let inline computeTurningBack (range: double) (distance: double): double =
            let integerPart = int(distance / range)
            let rem = distance % range
            let direction =
                match integerPart with
                | Even -> ToRight
                | Odd -> ToLeft
            match direction with
            | ToLeft -> range - rem
            | ToRight -> rem

        elapsedTime <- elapsedTime + double(delta)

        let x =
            elapsedTime * 200.0
            |> computeTurningBack 833.0

        match this.Get("position") :?> Vector2 with
        | Vector2(_, y) -> this.Set("position", Vector2(float32(96.0 + x), y))

type TimerFs() =
    inherit Timer()

    let mutable flag = true;

    override this._Ready() =
        let timer = this.GetNode<Timer>(new NodePath("/root/Main/Timer"))
        timer.Connect("timeout", this, "_OnTimerTimeout") |> ignore

    member this._OnTimerTimeout() =
        let icon = this.GetNode<Sprite>(new NodePath("/root/Main/Icon"))
        icon.Material.Set("shader_param/alpha", if flag then 1.0f else 0.4f)
        flag <- not flag
