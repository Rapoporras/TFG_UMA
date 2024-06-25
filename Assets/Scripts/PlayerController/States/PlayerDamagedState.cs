﻿using UnityEngine;

namespace PlayerController.States
{
    public class PlayerDamagedState : PlayerBaseState
    {
        private float _timeInState;
        
        public PlayerDamagedState(PlayerStates key, PlayerMovement context)
            : base(key, context) { }

        public override void EnterState()
        {
            _timeInState = 0f;
            
            InputManager.Instance.PlayerActions.Attack.Disable();
        }

        public override void UpdateState()
        {
            _timeInState += Time.deltaTime;
        }

        public override void FixedUpdateState() { }

        public override void ExitState()
        {
            Context.IsTakingDamage = false;
            Context.UseKnockBackAccelInAir = true;
            
            InputManager.Instance.PlayerActions.Attack.Enable();
        }

        public override PlayerStates GetNextState()
        {
            if (_timeInState >= Context.MovementData.knockBackDuration)
            {
                if (Context.IsGrounded)
                    return PlayerStates.Grounded;
                
                return PlayerStates.Falling;
            }
            
            return StateKey;
        }
    }
}