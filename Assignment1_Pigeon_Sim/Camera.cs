using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1_Pigeon_Sim
{
    class Camera
    {
        private Vector3 pitchVector = new Vector3(1, 0, 0);
        private Vector3 yawVector = new Vector3(0, 1, 0);
        private Vector3 rollVector = new Vector3(0, 0, 1);
        private Quaternion deltaQuaternion;
        
        public Camera(Vector3 deltaVector)
        {
            deltaQuaternion = new Quaternion(deltaVector.X, deltaVector.Y, deltaVector.Z, 0);
            deltaQuaternion.Normalize();
            Debug.WriteLine("deltaQuat : " + deltaQuaternion.X + " " + deltaQuaternion.Y + " " + deltaQuaternion.Z + " " + deltaQuaternion.W);
        }

        public Vector3 GetDeltaVector()
        {
            Vector3 deltaVector = new Vector3(deltaQuaternion.X, deltaQuaternion.Y, deltaQuaternion.Z);

            return deltaVector;
        }

        public Vector3 CameraUpdate(Vector3 positionVector, Vector3 inputVector)
        {
            Vector3 resultVector;
            Vector2 testVector = new Vector2(inputVector.X, inputVector.Y);
            
            if (inputVector.Length() > 0)
            {
                float radianX = CameraRadian(inputVector.X);
                float radianY = CameraRadian(inputVector.Y);
                float radianZ = CameraRadian(inputVector.Z);

                Vector3 deltaVector = new Vector3(deltaQuaternion.X, deltaQuaternion.Y, deltaQuaternion.Z);

                pitchVector = Vector3.Cross(deltaVector, yawVector);

                rollVector = deltaVector;
               
                pitchVector.Normalize();
                yawVector.Normalize();
                rollVector.Normalize();
                
                Quaternion pitchQuaternion = RotateCamera(radianY, pitchVector, deltaQuaternion);
                Debug.WriteLine("pitch Quat: " + pitchQuaternion.X + " " + pitchQuaternion.Y + " " + pitchQuaternion.Z + " " + pitchQuaternion.W);

                Quaternion yawQuaternion = RotateCamera(-radianX, yawVector, deltaQuaternion);
                Debug.WriteLine("yaw quat: " + yawVector.X + " " + yawVector.Y + " " + yawVector.Z);

                Quaternion rollQuaternion = RotateCamera(radianZ, rollVector, deltaQuaternion);
                Debug.WriteLine("roll quat: " + rollVector.X + " " + rollVector.Y + " " + rollVector.Z);

                pitchQuaternion.Normalize();
                yawQuaternion.Normalize();
                rollQuaternion.Normalize();

                // this is suspect
                Quaternion rotateQuaternion = Quaternion.Multiply(Quaternion.Multiply( pitchQuaternion, yawQuaternion), rollQuaternion);
                //Quaternion rotateQuaternion = Quaternion.Multiply(pitchQuaternion, yawQuaternion);
                rotateQuaternion.Normalize();



                Quaternion positionQuaternion = new Quaternion(positionVector.X, positionVector.Y, positionVector.Z, 0);
                Quaternion resultQuaternion = Quaternion.Multiply(rotateQuaternion, positionQuaternion);
                resultQuaternion.Normalize();

                resultVector = new Vector3(rotateQuaternion.X, rotateQuaternion.Y, rotateQuaternion.Z);

                Debug.WriteLine("result: " + resultVector.X + " " + resultVector.Y + " " + resultVector.Z);

                radianX = 0;
                radianY = 0;
                radianZ = 0;
                
                return resultVector;
            }
            else
            {
                resultVector = new Vector3(Quaternion.Identity.X, Quaternion.Identity.Y, Quaternion.Identity.Z);

                return resultVector;
            }
            
        }

        // qpq'
        private Quaternion RotateCamera(float inputAngle, Vector3 inputAxis, Quaternion pQuat)
        {
            
            Quaternion qQuat = AxisAngle(inputAngle, inputAxis);
            Debug.WriteLine("qQuat: " + qQuat.X + " " + qQuat.Y + " " + qQuat.Z + " " + qQuat.W);

            Debug.WriteLine("pQuat: " + pQuat.X + " " + pQuat.Y + " " + pQuat.Z + " " + pQuat.W);
            
            Quaternion pqQuat = Quaternion.Multiply(qQuat, pQuat);
            Debug.WriteLine("pqQuat: " + pqQuat.X + " " + pqQuat.Y + " " + pqQuat.Z + " " + pqQuat.W);

            Quaternion resultQuat =  Quaternion.Multiply(pqQuat, Quaternion.Inverse(qQuat));

            Debug.WriteLine("resultQuat: " + resultQuat.X + " " + resultQuat.Y + " " + resultQuat.Z + " " + resultQuat.W);

            deltaQuaternion = resultQuat;

            return resultQuat;
        }

        private Quaternion AxisAngle(float theTheta, Vector3 inputAxis)
        {
            Quaternion rotationQuart;
            double sinTheta;

            // Normalise rotation axis to have unit length
            inputAxis.Normalize();
            //Debug.WriteLine("input Axis: " + inputAxis.X + " "+ inputAxis.Y + " " + inputAxis.Z);

            sinTheta = Math.Sin(theTheta / 2);

            //Debug.WriteLine("sinTheta: " + sinTheta);

            // Convert to rotation quaternion
            rotationQuart.X = (float)(inputAxis.X * sinTheta);  // theTheta should be in radians
            rotationQuart.Y = (float)(inputAxis.Y * sinTheta);
            rotationQuart.Z = (float)(inputAxis.Z * sinTheta);
            rotationQuart.W = (float)(Math.Cos(theTheta / 2));

            //Debug.WriteLine("QangleAxis: " + rotationQuart.X + " " + rotationQuart.Y + " " + rotationQuart.Z + " "+ rotationQuart.W);

            return rotationQuart;
        }

        

        public float CameraRadian(float inputDegree)
        {
            return inputDegree * (float)(Math.PI / 180);
        }
    }
}
