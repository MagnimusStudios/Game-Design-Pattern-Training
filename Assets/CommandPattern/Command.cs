using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Animator Anim);
}

public class PerformJump : Command
{
    public override void Execute(Animator Anim)
    {
        Anim.SetTrigger("isJumping");
    }
}

public class PerformPunch : Command
{
    public override void Execute(Animator Anim)
    {
        Anim.SetTrigger("isPunching");
    }
}

public class PerformKick : Command
{
    public override void Execute(Animator Anim)
    {
        Anim.SetTrigger("isKicking");
    }
}

public class PerformWalk : Command
{
    public override void Execute(Animator Anim)
    {
        Anim.SetTrigger("isWalking");
    }
}

public class DoNothing : Command
{
    public override void Execute(Animator Anim)
    {
        
    }
}