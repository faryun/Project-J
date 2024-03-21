public class DiceEye
{
    public int value; // 기본 값
    public int grade; // 등급 (희귀도)
    public string effect; // 효과 설명
    public string[] tags; // 태그 배열

    public DiceEye(int value, int grade, string effect, string[] tags)
    {
        this.value = value;
        this.grade = grade;
        this.effect = effect;
        this.tags = tags;
    }
}

public class DiceList
{
    public static DiceEye[] defaultEyes = new DiceEye[]
    {
        new DiceEye(1, 1, "목표값에 1을 더한다.", new string[] {}),
        new DiceEye(2, 1, "목표값에 2를 더한다.", new string[] {}),
        new DiceEye(3, 1, "목표값에 3을 더한다.", new string[] {}),
        new DiceEye(4, 1, "목표값에 4를 더한다.", new string[] {}),
        new DiceEye(5, 1, "목표값에 5를 더한다.", new string[] {}),
        new DiceEye(6, 1, "목표값에 6을 더한다.", new string[] {})
    };

    public static DiceEye treasureBox = new DiceEye(1, 2, "목표값에 1을 더한다. 열쇠와 함께 나오면 추가로 10을 더한다.", new string[] { "보물상자" });
    public static DiceEye key = new DiceEye(1, 2, "목표값에 1을 더한다.", new string[] { "열쇠" });
    public static DiceEye seven = new DiceEye(1, 2, "목표값에 1을 더한다. 인접한 주사위에 7이 두개 있으면 추가로 74를 더한다.", new string[] { "7" });
    public static DiceEye hongSalmon = new DiceEye(0, 3, "여고생. 물고기. 버프 '헉'을 얻는다. 헉: 3개 모이면 0으로 초기화되고 목표값에 12를 더한다.", new string[] { "여고생", "물고기", "헉" });
    public static DiceEye napizak = new DiceEye(2, 3, "여고생. 새. 목표값에 2를 더한다. 홍연어나 오버워치가 인접하면 파괴하고 목표값에 10을 더한다.", new string[] { "여고생", "새" });
    public static DiceEye overwatch = new DiceEye(0, 3, "파괴되면 목표값에 10을 더한다.", new string[] { "파괴" });
    public static DiceEye longBird = new DiceEye(0, 3, "새. 이 눈에 모자가 6 이상이라면 목표값에 8, 아니면 2를 더하고 버프 '모자'를 이 눈에 추가한다.", new string[] { "새", "모자" });
}