using BasicShmup.Dynamics;
using Godot;

namespace BasicShmup.Input.Controllers;

public class CharacterBodyControllerStrategy : ICharacterBodyControllerStrategy
{
    public void Move(CharacterBody2D body, Speed speed)
    {
        var movement = speed * InputActions.GetClampedMovement();
        body.Velocity = movement;
        body.MoveAndSlide();
    }
}
