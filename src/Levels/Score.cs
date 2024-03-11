/*
Copyright (C) 2024 Ruby-Dragon
This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Godot;
using System;

public partial class Score : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	public Label Refrain;

	private bool failed = false;
	
	[Export] 
	public RichTextLabel ScoreLabel;
	
	public override void _Ready()
	{
		int Score = GetNode<SharedLevelData>("/root/SharedLevelData").LevelScore;
		
		if (Score < 100)
		{
			Refrain.Visible = true;
		}

		ScoreLabel.Text = "[center]Score: " + Score + "%, Grade: ";

		switch (Score)
		{
			case (>=90):
				ScoreLabel.Text = ScoreLabel.Text + "A";
				break;
			case (>=80):
				ScoreLabel.Text = ScoreLabel.Text + "B";
				break;
			case (>=70):
				ScoreLabel.Text = ScoreLabel.Text + "C";
				break;
			case (>=60):
				ScoreLabel.Text = ScoreLabel.Text + "D";
				break;
			default:
				ScoreLabel.Text = ScoreLabel.Text + "Unacceptable.\nTry Again.";
				failed = true;
				break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("Interact"))
		{
			if (failed)
			{
				GetTree().ChangeSceneToPacked(GetNode<SharedLevelData>("/root/SharedLevelData").LastLevel);
				return;
			}
			GetTree().ChangeSceneToPacked(GetNode<SharedLevelData>("/root/SharedLevelData").NextLevel);
		}
	}
}
