using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapon")]
// Thuộc tính này tạo một mục menu trong Unity Editor để tạo các instance mới của lớp này dưới dạng ScriptableObjects.
// Tham số `fileName` chỉ định tên tệp mặc định cho asset được tạo.
// Tham số `menuName` chỉ định đường dẫn trong menu Tạo Asset nơi mục sẽ xuất hiện.

public class WeaponData : ItemData
// Dòng này khai báo một lớp công khai có tên `WeaponData` kế thừa từ lớp `ItemData`.
// Lớp này có thể chứa dữ liệu liên quan đến vũ khí trong game.

{
    public GameObject controller;
    // Biến công khai này lưu trữ tham chiếu đến một GameObject điều khiển hành vi của vũ khí.

    public GameObject prefab;
    // Biến công khai này lưu trữ tham chiếu đến một GameObject prefab đại diện cho hình thức trực quan của vũ khí.

    public float speed;
    // Biến công khai này lưu trữ tốc độ của đạn hoặc đòn tấn công của vũ khí.

    public float damage;
    // Biến công khai này lưu trữ lượng sát thương mà vũ khí gây ra cho kẻ địch.

    public float cooldownDuration;
    // Biến công khai này lưu trữ thời gian hồi chiêu của vũ khí giữa các lần tấn công.

    public int pierce;
    // Biến công khai này lưu trữ số lượng kẻ địch mà vũ khí có thể xuyên qua với một lần tấn công.

    public float lifeTime;
    // Biến công khai này lưu trữ thời gian tồn tại của đạn hoặc hiệu ứng của vũ khí.

    public int damagePlus;
    // Biến công khai này lưu trữ lượng sát thương thưởng có thể được thêm vào sát thương cơ bản của vũ khí.
}