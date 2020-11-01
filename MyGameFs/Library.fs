namespace MyGameFs

open Godot

type IconFs() =
    inherit Node()

    override this._Ready() =
        GD.Print("Hello from F#!")

type ButtonFs() =
    inherit Button()

    let mutable count = 0

    member this._OnButtonPressed() =
        let label: Label = downcast this.GetNode(new NodePath("/root/Main/Label"))
        label.Text <-
            if count > 0
            then sprintf "PRESSED! (%d)" count
            else "PRESSED!"
        count <- count + 1
