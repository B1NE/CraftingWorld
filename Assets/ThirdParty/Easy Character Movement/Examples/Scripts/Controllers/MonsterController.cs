using ECM.Common;
using ECM.Controllers;
using UnityEngine;

namespace ECM.Examples
{
    /// <summary>
    /// 
    /// Example Character Controller
    /// 
    /// This example shows how to extend the 'BaseCharacterController' adding support for different
    /// character speeds (eg: walking, running, etc), plus how to handle custom input extending the
    /// HandleInput method and make the movement relative to camera view direction.
    /// 
    /// </summary>

    public sealed class MonsterController : BaseCharacterController
    {
        #region METHODS

        /// <summary>
        /// Overrides 'BaseCharacterController' Animate method.
        /// 
        /// This shows how to handle your characters' animation states using the Animate method.
        /// The use of this method is optional, for example you can use a separate script to manage your
        /// animations completely separate of movement controller.
        /// 
        /// </summary>

        protected override void Animate()
        {
            // If no animator, return

            if (animator == null)
                return;

            // Compute move vector in local space

            var move = transform.InverseTransformDirection(moveDirection);

            // Update the animator parameters

            var forwardAmount = animator.applyRootMotion
                ? move.z
                : Mathf.InverseLerp(0.0f, 0.0f, movement.forwardSpeed);

            //animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
            //animator.SetFloat("Turn", Mathf.Atan2(move.x, move.z), 0.1f, Time.deltaTime);

            animator.SetBool("OnGround", movement.isGrounded);
            //animator.SetBool("Attack", attack);

            if (!movement.isGrounded)
                animator.SetFloat("Jump", movement.velocity.y, 0.1f, Time.deltaTime);
        }

        /// <summary>
        /// Overrides 'BaseCharacterController' HandleInput,
        /// to perform custom controller input.
        /// </summary>

        protected override void HandleInput()
        {

        }

        #endregion
    }
}
