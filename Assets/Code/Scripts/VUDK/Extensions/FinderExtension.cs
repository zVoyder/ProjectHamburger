namespace VUDK.Extensions
{
    using UnityEngine;

    public static class FinderExtension
    {
        /// <summary>
        /// Get the closest gameobject in an array of gameobjects.
        /// </summary>
        /// <param name="self">transform source.</param>
        /// <param name="gameObjetcs">array of the gameobjects.</param>
        /// <returns>the closest GameObject.</returns>
        public static GameObject GetClosestGameObject(this GameObject self, GameObject[] gameObjetcs)
        {
            GameObject tMin = null;
            float minDist = Mathf.Infinity; // I set the minDist to Infinity so it will be overwritten
            Vector3 currentPos = self.transform.position;

            foreach (GameObject t in gameObjetcs) //Loop for checking who is the closest
            {
                float dist = Vector3.Distance(t.transform.position, currentPos); // I get the distance
                if (dist < minDist) // if the distance is less of the minimum distance
                {
                    tMin = t; // the closest gameobject for now
                    minDist = dist; // minimum distance for now
                }
            }

            return tMin;
        }

        /// <summary>
        /// Tries to get the closest gameobject in an array of gameobjects found with a tag.
        /// </summary>
        /// <param name="self">transform source.</param>
        /// <param name="tag">tag of the gameobjects.</param>
        /// <param name="closest">closestgameobject.</param>
        /// <returns>True if the gameobject was found, False if not.</returns>
        public static bool TryGetClosestGameObjectWithTag(this GameObject self, string tag, out GameObject closest)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

            if (gameObjects.Length > 0)
            {
                closest = GetClosestGameObject(self, gameObjects);
                return true;
            }

            closest = null;
            return false;
        }

        /// <summary>
        /// Tries to find a GameObject with a specified Tag.
        /// </summary>
        /// <param name="tag">Tag name.</param>
        /// <param name="gObject">Found Gameobject.</param>
        /// <returns>True if it was found, False if not.</returns>
        public static bool TryFindGameObjectWithTag(string tag, out GameObject gObject)
        {
            try
            {
                gObject = GameObject.FindGameObjectWithTag(tag); //Try to take it
            }
            catch 
            {
                gObject = null;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to find a GameObject with a specified name.
        /// </summary>
        /// <param name="name">Tag name.</param>
        /// <param name="gObject">Found Gameobject.</param>
        /// <returns>True if it was found, False if not.</returns>
        public static bool TryFindGameObject(string name, out GameObject gObject)
        {
            try
            {
                gObject = GameObject.Find(name); //Try to take it
            }
            catch (System.Exception)
            {
                gObject = null;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Destroys the last component of type T in this GameObject.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        public static void DestroyLastComponentOfType<T>(this GameObject gameObject) where T : Component
        {
            T[] components = gameObject.GetComponents<T>();

            if (components.Length > 0)
            {
                GameObject.Destroy(components[components.Length - 1]);
            }
        }
    }
}
