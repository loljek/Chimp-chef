using UnityEngine;

namespace InteractableObjects
{
    [RequireComponent(typeof(BoxCollider))]

    public abstract class PlacementObject : InteractableObject
    {
        [SerializeField] private PlacementObject objectModel;
        [SerializeField] private Material good;
        [SerializeField] private Material bad;
        [SerializeField] public bool isBox;
        private PlacementObject replaceObject;
        private Transform replaceTrans;
        private Transform trans;
        public Place place;
        public Place curPlace;
        public bool isReplacing;
        private bool isOnPlace;
        public bool isReady;
        private bool isBadMaterial;
        
        private void Awake()
        {
            interactSpace = GetComponent<BoxCollider>();
            if (objectModel != null)
            {
                objectModel = GetComponent<PlacementObject>();
            }
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
            if (objectModel == null) return;
            trans = pos;
            place = objectModel?.GetCurPlace();
            replaceObject = Instantiate(objectModel, trans.position, objectModel!.transform.rotation);
            replaceObject.gameObject.SetActive(true);
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
            SetCurPlace(null);
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
            if (isBox)
            {
                objectModel.gameObject.SetActive(true);
                SetObjectModel(null);
            }
        }

        private void ReplaceGood()
        {
            isReady = true;
            replaceTrans.position = new Vector3(curPlace.GetPos().x, curPlace.GetPos().y, curPlace.GetPos().z);
            if (!isBadMaterial) return;
            ChangeMaterial(replaceTrans.gameObject, good);
            isBadMaterial = false;
        }
        
        private void ReplaceBad()
        {
            replaceTrans.position = trans.position;
            isReady = false;
            if (isBadMaterial) return;
            ChangeMaterial(replaceTrans.gameObject, bad);
            isBadMaterial = true;
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

        public void SetObjectModel(PlacementObject p)
        {
            objectModel = p;
        }
        
        public PlacementObject GetObjectModel()
        {
            return objectModel;
        }

        private Place GetCurPlace()
        {
            return curPlace;
        }
        
        public override void Interaction()
        {
            PlacementObjectInteraction();
        }

        protected abstract void PlacementObjectInteraction();
    }
}
