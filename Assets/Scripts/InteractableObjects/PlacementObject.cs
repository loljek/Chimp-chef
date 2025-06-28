using UnityEngine;

namespace InteractableObjects
{
    [RequireComponent(typeof(BoxCollider))]

    public abstract class PlacementObject : InteractableObject
    {
        [SerializeField] private PlacementObject objectModel;
        [SerializeField] private Material good;
        [SerializeField] private Material bad;
        private PlacementObject replaceObject;
        private Transform replaceTrans;
        private Transform trans;
        public Place place;
        public Place curPlace;
        public bool isReplacing;
        private bool isOnPlace;
        public bool isReady;
        private bool isBadMaterial;
        private float objectPos;
        
        private void Awake()
        {
            interactSpace = GetComponent<BoxCollider>();
            objectPos = transform.position.y;
        }

        private void Update()
        {
            if (isReplacing & isOnPlace)
            {
                if (!curPlace.isOccupied)
                {
                    ReplaceGood();
                } 
                else
                {
                    ReplaceBad();
                }
            }           
            else if (isReplacing)
            {
                ReplaceBad();
            }
        }

        public void ReplaceStart (Transform pos)
        {
            trans = pos;
            place = objectModel.GetCurPlace();
            replaceObject = Instantiate(objectModel, trans.position, objectModel.transform.rotation);
            replaceTrans = replaceObject.GetComponent<Transform>();
            ChangeCollider(replaceTrans, false);
            ChangeMaterial(replaceTrans.gameObject, bad);
            isBadMaterial = true;
            isReplacing = true;
        }

        public void ReplaceCancel()
        {
            Destroy(replaceObject.gameObject);
            replaceObject = null;
            isReplacing = false;
            isReady = false;
            curPlace = place;
        }

        public void ReplaceEnd()
        { 
            place?.ChangeIsOccupied();
            curPlace.ChangeIsOccupied();
            objectModel.SetPlace(curPlace);
            objectModel.transform.position = replaceTrans.position;
            objectModel.transform.rotation = replaceTrans.rotation;
            Destroy(replaceObject.gameObject);
            isReplacing = false;
            isReady = false;
        }

        private void ReplaceGood()
        {
            isReady = true;
            replaceTrans.position = new Vector3(curPlace.GetPos().x, objectPos, curPlace.GetPos().z);
            if (isBadMaterial)
            {
                ChangeMaterial(replaceTrans.gameObject, good);
                isBadMaterial = false;
            }
        }
        
        private void ReplaceBad()
        {
            replaceTrans.position = trans.position;
            isReady = false;
            if (!isBadMaterial)
            {
                ChangeMaterial(replaceTrans.gameObject, bad);
                isBadMaterial = true;
            }
        }

        public void Rotate()
        {
            if (isReplacing)
            {
                replaceTrans.Rotate(new Vector3(0, 90, 0));
            }
            else
            {
                objectModel.transform.Rotate(new Vector3(0, 90, 0));
            }
        }

        private static void ChangeMaterial(GameObject root, Material m)
        {
            Renderer r = root.GetComponent<Renderer>();
            if (r != null)
            {
                r.material = m;
            }

            foreach (Transform child in root.transform)
            {
                ChangeMaterial(child.gameObject, m);
            }
        }

        public void SetCurPlace(Place p)
        {
            if (p != null)
            {
                curPlace = p;
                isOnPlace = true;
            }
            else
            {
                isOnPlace = false;
            }
        }

        private void SetPlace(Place p)
        {
            place = p;
        }

        private Place GetCurPlace()
        {
            return curPlace;
        }

        private void ChangeIsReplacing()
        {
            isReplacing = !isReplacing;
        }
        
        public override void Interaction()
        {
            PlacementObjectInteraction();
        }

        protected abstract void PlacementObjectInteraction();
    }
}
