using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class LoadImage : MonoBehaviour
{
    public Canvas inputCanvas;
    public GameObject inputImageDisplay;
    public GameObject inputButton;

    public static Canvas curCanvas;
    public static GameObject imageDisplay;
    private static string imageFolder;
    public static GameObject button;

    private static Object[] images;
    private static int imageIndex;
    private static Vector2 startMousePos;

    private static System.DateTime lastInteractionTime;
    private readonly int TRACKING_MAX_TIME = 5;

    // Use this for initialization
    public void Start()
    {
        curCanvas = inputCanvas;
        imageDisplay = inputImageDisplay;
        button = inputButton;

        Debug.Log("START! button: " + button.name);
        curCanvas.enabled = false;
        imageDisplay.SetActive(false);
        button.SetActive(false);
        imageIndex = 0;
    }

    public static void Show(string folderLoc)
    {
        imageFolder = folderLoc;
        button.SetActive(true);
        if (imageFolder.Length != 0)
        {
            LoadImagesFromFolder();
            DisplayImage();
        }
        else
        {
            Debug.LogError("no images to display");
        }
    }

    public void StopShowing()
    {
        imageDisplay.SetActive(false);
        button.SetActive(false);
    }

    private static void LoadImagesFromFolder()
    {
        Debug.Log("ImageFolder:  " + imageFolder);
        images = Resources.LoadAll(imageFolder, typeof(Texture2D));
        Debug.Log("images length: " + images.Length);
    }

    private static void DisplayImage()
    {
        // change texture to next image in folder
        Texture2D texture = (Texture2D)images[imageIndex];
        Debug.Log("index: " + imageIndex + ", texture: " + texture.name);
        UnityEngine.UI.Image imageRenderer = imageDisplay.GetComponent<UnityEngine.UI.Image>();
        //if(texture.width >= texture.height)
        //{
        //    if(texture.width > Screen.width)
        //    {
        //        int scaleFactor = texture.width / Screen.width;
        //        texture.width /= scaleFactor;
        //        texture.height /= scaleFactor;
        //    }
        //}
        //else
        //{
        //    if(texture.height > Screen.height)
        //    {
        //        int scaleFactor = texture.height / Screen.height;
        //        texture.width /= scaleFactor;
        //        texture.height /= scaleFactor;
        //    }
        //}
        Sprite imageSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
        imageRenderer.sprite = imageSprite;
        //Renderer imageRenderer = imageDisplay.GetComponent<MeshRenderer>();
        //imageRenderer.material.mainTexture = texture;
        imageRenderer.preserveAspect = true;

        //imageRenderer.material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        //imageRenderer.material.SetFloat("_SpecularHighlights", 1f);
        curCanvas.enabled = true;
        imageDisplay.SetActive(false);
        imageDisplay.SetActive(true);

        lastInteractionTime = System.DateTime.Now;
    }
    

    // Update is called once per frame
    void Update()
    {
        if ((System.DateTime.Now - lastInteractionTime).Seconds > TRACKING_MAX_TIME)
        {
            StopShowing();
        }
    }


    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        DraggedDirection draggedDir;
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        Debug.Log("DRAGGED: " + draggedDir);
        return draggedDir;
    }

    public void OnMouseDown()
    {
        Debug.Log("Mouse Down!");
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        Debug.Log("FLOATS x: " + x + "   y: " + y);


        startMousePos = new Vector2(x, y);
        Debug.Log("START x: " + startMousePos.x + "   y: " + startMousePos.y);
    }

    public void OnEndDrag()
    {
        Debug.Log("Mouse Drag!");
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        Vector2 endMousePos = new Vector2(x, y);

        Debug.Log("END x: " + endMousePos.x + "   y: " + endMousePos.y);
        Vector2 dragVectorDirection = (endMousePos - startMousePos).normalized;
        Debug.Log("norm + " + dragVectorDirection);
        DraggedDirection dir = GetDragDirection(dragVectorDirection);
        Swiped(dir == DraggedDirection.Right);
    }

    private void Swiped(bool right)
    {
        Debug.Log("SWIPED");
        if (right)
            imageIndex++;
        else imageIndex--;
        if (imageIndex >= images.Length)
        {
            imageIndex = 0;
        }
        else if (imageIndex < 0)
        {
            imageIndex = images.Length - 1;
        }
        DisplayImage();
    }
}
