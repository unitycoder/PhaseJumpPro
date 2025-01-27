﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * RATING: 5 stars. Simple utility class for 2D games with Unit Tests
 * CODE REVIEW: 4.8.22
 */
namespace PJ
{
	/// <summary>
	/// Provides utility methods for simplifying common 2D game scenarios
	/// </summary>
	public class Node2D : SomeNode
	{
		/// <summary>
		/// Type for velocity
		/// </summary>
		public enum MoveType
		{
			// No movement
			None,

			// Moves in direction it is facing (based on 0 degree meaning up)
			Forward,

			// Moves based on vector
			Vector
		}

		[Header("Node2D Properties")]

		[SerializeField]
		[Range(0, 360.0f)]
		protected float rotationDegreeAngle = 0;

		protected SpriteRenderer spriteRenderer;

		public override bool IsKinematic
        {
			get
            {
				return rigidbody == null || rigidbody.isKinematic;
            }
        }

		public Vector2 Velocity
        {
			get => velocity;
			set
            {
				velocityType = MoveType.Vector;
				velocity = value;

				if (null != maxVelocity)
				{
					velocity = Vector2.ClampMagnitude(velocity, maxVelocity.value);
				}

				UpdateVelocity();
			}
        }

		public float ForwardVelocity
        {
			get => velocity.x;
			set
            {
				velocityType = MoveType.Forward;

				velocity.x = value;
				velocity.y = 0;

				if (null != maxVelocity)
				{
					velocity = Vector2.ClampMagnitude(velocity, maxVelocity.value);
				}

				UpdateVelocity();
			}
        }

		public Vector2 Acceleration
		{
			get => acceleration;
			set
			{
				acceleration = value;
			}
		}

		public float ForwardAcceleration
        {
			get => acceleration.x;
			set
            {
				acceleration.x = value;
				acceleration.y = 0;
            }
        }

		public float MaxVelocity
		{
			set
			{
				maxVelocity = new Optional<float>(value);
			}
		}

		public MoveType VelocityType => velocityType;

		/// <summary>
		/// If true, modulate rotation values (stay within range of 0-1.0)
		/// </summary>
		protected bool clipRotation = true;

		[SerializeField]
		protected MoveType velocityType = MoveType.None;

		[SerializeField]
		protected Vector2 velocity = new Vector2(0, 0);

		protected Vector2 acceleration = Vector2.zero;
		protected Optional<float> maxVelocity;
		protected new Rigidbody2D rigidbody;

		// Normalized rotation value (0-1.0)
		public float RotationNormal
		{
			get { return rotationDegreeAngle / 360.0f; }
			set
			{
				rotationDegreeAngle = value * 360.0f;
			}
		}

		public float RotationDegreeAngle
		{
			get { return rotationDegreeAngle; }
			set
			{
				var newAngle = value;
				if (clipRotation)
				{
					newAngle = AngleUtils.ClipDegreeAngle(newAngle);
				}

				if (rotationDegreeAngle == newAngle) { return; }
				rotationDegreeAngle = newAngle;

				// If we are moving forward, update the velocity when the value changes
				switch (velocityType)
				{
					case MoveType.Forward:
						var velocityVector = AngleUtils.DegreeAngleToVector2(rotationDegreeAngle, velocity.x);
						rigidbody.velocity = velocityVector;
						break;
					default:
						break;
				}

				// Try-catch is for the unit test
				try
				{
					if (transform)
					{
						transform.localEulerAngles = new Vector3(0, 0, -rotationDegreeAngle);
					}
				}
				catch (NullReferenceException ex)
				{
					Debug.Log(ex.Message);
				}
			}
		}

        protected override void OnValidate()
        {
            base.OnValidate();

			transform.localEulerAngles = new Vector3(0, 0, -RotationDegreeAngle);
		}

        protected override void Awake()
        {
            base.Awake();

			spriteRenderer = GetComponent<SpriteRenderer>();
			rigidbody = GetComponent<Rigidbody2D>();
		}

		protected override void Start()
		{
			base.Start();

			UpdateVelocity();
		}

		protected virtual void UpdateVelocity()
        {
			if (null == rigidbody) { return; }
			if (rigidbody.bodyType == RigidbodyType2D.Static) { return; }

			Vector2 velocityVector = Vector2.zero;
			switch (velocityType)
			{
				case MoveType.None:
					return;
				case MoveType.Forward:
					velocityVector = AngleUtils.DegreeAngleToVector2(RotationDegreeAngle, velocity.x);
					break;
				case MoveType.Vector:
					velocityVector = velocity;
					break;
			}

			rigidbody.velocity = velocityVector;
		}

		protected override void FixedUpdate()
        {
            base.FixedUpdate();

			CheckAcceleration(new TimeSlice(Time.deltaTime), UpdateType.Fixed);
        }

		protected override void OnUpdate(TimeSlice time)
		{
			base.OnUpdate(time);

			CheckAcceleration(new TimeSlice(Time.deltaTime), UpdateType.Default);
		}

		protected void CheckAcceleration(TimeSlice time, UpdateType updateType)
		{
			// Check velocity
			if (!ShouldMoveForUpdate(updateType)) { return; }

			if (Vector2.zero == acceleration) { return; }

			switch (velocityType)
			{
				case MoveType.Forward:
					ForwardVelocity = ForwardVelocity + acceleration.x * time.delta;
					break;
				default:
					Velocity = velocity + acceleration * time.delta;
					break;
			}
		}

		protected bool ShouldMoveForUpdate(UpdateType updateType)
		{
			bool isFixedUpdate = updateType == UpdateType.Fixed;

			var isKinematic = IsKinematic;

			// Kinematic objects move in Update
			// Physics objects move in FixedUpdate
			if (isKinematic)
			{
				return !isFixedUpdate;
			}

			return isFixedUpdate;
		}

		/// <summary>
        /// Move this node to the position in 2D space
        /// </summary>
		public void MoveToPosition(Vector3 position, bool force = false)
		{
			if (IsKinematic || force)
			{
				//Debug.Log("Move to Position: Kinematic");

				transform.position = position;
			}
			else
			{
				if (null != rigidbody)
				{
					//Debug.Log("Move to Position: Physics");

					// MovePosition is for physics-based objects (non kinematic)
					rigidbody.MovePosition(position);
				}
			}
		}

		public virtual void OnTransformLimited() { }
	}
}
