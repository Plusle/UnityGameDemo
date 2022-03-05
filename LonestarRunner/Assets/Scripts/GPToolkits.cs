using UnityEngine;

public class GPToolkits {
    public static Quaternion FixedRotation = Quaternion.Euler(-90, 0, 0);

    public static int GetRandomIndexWithinInterval(int min, int max) {
        return Random.Range(min, max);
    }

    public static int GetRandomIndexWithinArray(GameObject[] objects) {
        return GetRandomIndexWithinInterval(0, objects.Length);
    }

    public static GameObject GetRandomObjectWithinArray(GameObject[] objects) {
        return objects[GetRandomIndexWithinArray(objects)];
    }

    public static GameObject GetRandomObjectWithinArray(GameObject[] objects, int min, int max) {
        if (min < 0 || max > objects.Length) throw new UnityException("Illegal index for array");
        return objects[GetRandomIndexWithinInterval(min, max)];
    }

    public static Vector3 GetRandomVec3WithinAABB(Vector3 min, Vector3 max) {
        return new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), Random.Range(min.z, max.z));
    }
    
    public static Vector3 GetRandomVec3WithinAABB(Transform min, Transform max) {
        return GetRandomVec3WithinAABB(min.position, max.position);
    }

    public static Vector3 GetRandomVec3WithinColliderAABB(Collider collider) {
        Vector3 pos = new Vector3(
                Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                Random.Range(collider.bounds.min.y, collider.bounds.max.y),
                Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        return pos;
    }

    public static bool IsInsideAABB(Vector3 vec, Collider collider) {
        if (vec.x > collider.bounds.min.x && vec.x < collider.bounds.max.x)
            return true;
        if (vec.y > collider.bounds.min.y && vec.y < collider.bounds.max.y)
            return true;
        if (vec.z > collider.bounds.min.z && vec.z < collider.bounds.max.z)
            return true;
        return false;
    }

    //public static Vector3 GetRandomDirectionInHemisphere(Vector3 up) {
    //    Vector3 right;
    //    Vector3 forward;

    //    return new Vector3();
    //}

    public static int GetSign(float num) {
        return num >= 0.0f ? 1 : -1;
    }

    public static int GetInverseSign(float num) {
        return num >= 0.0f ? -1 : 1;
    }
} 