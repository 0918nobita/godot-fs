namespace MyGameFs

open Godot

type IconFs() =
    inherit Node()

    override this._Ready() =
        GD.Print("Hello from F#!")

type ButtonFs() =
    inherit Button()

    member this._OnButtonPressed() =
        GD.Print("Pressed!")
