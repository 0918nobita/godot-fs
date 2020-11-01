namespace MyGameFs

open Godot

type IconFs() =
    inherit Node()

    override this._Ready() =
        GD.Print("Hello from F#!")
