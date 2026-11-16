using System;
using System.Collections.Generic;
using System.Text;

public class PlayState_Pause : PlayState
{
    //
    public PlayState_Pause(EPlayState state) : base(state)
    {
    }

    //
    override public void Enter()
    {
        Manager_Game.StopGameTimer();
    }

    //
    override public void Exit()
    {

    }

    //
    override public void Update()
    {

    }
}
