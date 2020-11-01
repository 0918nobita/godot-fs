namespace MyGameFs

open Godot

type ButtonFs() =
    inherit Button()

    let mutable count = 0

    override this._Ready() =
        GD.Print("Hello from F#!")

    member this._OnButtonPressed() =
        let label: Label = downcast this.GetNode(new NodePath("/root/Main/Label"))
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
