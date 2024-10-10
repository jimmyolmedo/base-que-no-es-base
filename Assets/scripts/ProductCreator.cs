using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class ProductCreator : MonoBehaviour
{
    [SerializeField] Product prefab;
    [SerializeField] Transform parent;

    [SerializeField] TMP_InputField nombre;
    [SerializeField] TMP_InputField precio;
    [SerializeField] TMP_InputField stock;
    [SerializeField] Image sprite;

    public RawImage imageDisplay;

    void Start()
    {
        // Comprobar si el permiso ya ha sido otorgado
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            // Solicitar permiso
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }


    public void Create()
    {
        Item item = new Item();

        item.Name = nombre.text;
        item.price = int.Parse(precio.text);
        item.stock = int.Parse(stock.text);
        item.sprite = sprite.sprite;

        Product obj = Instantiate(prefab, parent);
        obj.item = item;

    }

    public void PickImage()
    {

        // Verificar si ya se ha otorgado el permiso de lectura
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            // Llamar a la galería para seleccionar una imagen
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
            {
                if (path != null)
                {
                    // Cargar la imagen seleccionada en un Texture2D
                    Texture2D texture = NativeGallery.LoadImageAtPath(path);
                    if (texture == null)
                    {
                        Debug.Log("No se pudo cargar la imagen.");
                        return;
                    }

                    // Mostrar la imagen en la UI
                    Sprite icon = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                    sprite.sprite = icon;

                }
                else
                {
                    Debug.Log("No se seleccionó ninguna imagen.");
                }
            });

            Debug.Log("Resultado del permiso: " + permission);
        }
        else
        {
            // Si el permiso no ha sido otorgado, solicitarlo al usuario
            Permission.RequestUserPermission(Permission.ExternalStorageRead);

            // Opcional: puedes mostrar un mensaje o repetir el intento de selección de imagen después de que el permiso sea concedido
            Debug.Log("El permiso de acceso a la galería no ha sido otorgado. Solicitando permiso...");
        }
    }
}
