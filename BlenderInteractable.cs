using UnityEngine;

public class BlenderInteractable : Interactable
{
    protected override void Interact()
    {
        Debug.Log("Interaksi berhasil dengan objek Blender!");
        // Tambahkan logika interaksi di sini
        // Contoh: mainkan animasi, buka panel, ubah material, dll.
    }
}
