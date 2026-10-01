Câu 1: Phân biệt Value Types và Reference Types trong C#
1. Value Types (Kiểu giá trị)
•	Lưu trữ trực tiếp giá trị của biến.
•	Thường được lưu trên Stack nếu là biến cục bộ.
•	Khi gán biến này cho biến khác, giá trị được sao chép.
Ví dụ: int, float, double, bool, char, struct.
2. Reference Types (Kiểu tham chiếu)
•	Biến lưu địa chỉ tham chiếu đến đối tượng, chứ không chứa trực tiếp toàn bộ đối tượng.
•	Đối tượng thường được lưu trên Heap, còn biến tham chiếu cục bộ thường nằm trên Stack.
•	Khi gán biến này cho biến khác, địa chỉ tham chiếu được sao chép nên cả hai có thể cùng trỏ đến một đối tượng.
Ví dụ: class, string, array, object.
3. Bảng so sánh
Tiêu chí	Value Types	Reference Types
Cách lưu trữ	Lưu trực tiếp giá trị	Lưu địa chỉ tham chiếu
Vùng nhớ	Thường là Stack	Đối tượng thường ở Heap
Khi gán biến	Sao chép giá trị	Sao chép địa chỉ
Ví dụ	int, double, bool	class, string, array
4. Ví dụ dễ hiểu
Value Types:
int a = 10;
int b = a;
b = 20;
// a = 10, b = 20
Vì b nhận bản sao giá trị của a, nên thay đổi b không làm thay đổi a.
Reference Types:
class Student
{
    public int age;
}

Student a = new Student();
a.age = 18;

Student b = a;
b.age = 20;
// a.age = 20
Vì a và b cùng tham chiếu đến một đối tượng nên thay đổi thông qua b cũng ảnh hưởng đến đối tượng mà a nhìn thấy.
Kết luận: Value Types lưu trực tiếp giá trị, còn Reference Types lưu tham chiếu đến đối tượng. Thông thường, biến cục bộ nằm trên Stack và đối tượng Reference Type nằm trên Heap. Tuy nhiên, đây là cách mô tả phổ biến để học cơ bản; vị trí thực tế còn phụ thuộc vào ngữ cảnh và cách chương trình được thực thi.

Câu 2: Phân biệt Init-only Properties (init) và thuộc tính set trong C#
1. Thuộc tính set thông thường
•	Cho phép gán và thay đổi giá trị sau khi tạo đối tượng.
•	Có thể cập nhật nhiều lần trong quá trình chương trình chạy.
Ví dụ:
class Student
{
    public string Name { get; set; }
}

Student sv = new Student();
sv.Name = "Dat";
sv.Name = "Minh Dat"; // Đổi được
2. Thuộc tính init
•	Chỉ cho phép gán giá trị khi khởi tạo đối tượng.
•	Sau khi khởi tạo xong, không thể gán lại giá trị bằng init.
Ví dụ:
class Student
{
    public string Name { get; init; }
}

Student sv = new Student
{
    Name = "Dat"
};

// sv.Name = "Minh Dat"; // Lỗi
3. Bảng so sánh
Tiêu chí	set	init
Khi khởi tạo	Có	Có
Thay đổi sau khởi tạo	Có	Không
Mục đích	Cho phép cập nhật	Giữ giá trị ổn định sau khi tạo
4. Trường hợp sử dụng thực tế
•	set: Dùng cho thông tin cần thay đổi thường xuyên, ví dụ điểm số hoặc địa chỉ hiện tại của sinh viên.
•	init: Dùng cho thông tin thường không muốn thay đổi sau khi tạo, ví dụ mã sinh viên hoặc ngày tạo đơn hàng.
Kết luận: set cho phép thay đổi giá trị bất cứ lúc nào, còn init chỉ cho phép gán giá trị lúc khởi tạo đối tượng. init giúp hạn chế việc vô tình sửa đổi dữ liệu sau khi tạo.

Câu 3: Phân biệt virtual và override trong C#
1. virtual (ở lớp cha)
•	Dùng để khai báo phương thức có thể được lớp con ghi đè.
•	Lớp cha cung cấp cách thực hiện mặc định.
2. override (ở lớp con)
•	Dùng để ghi đè phương thức virtual của lớp cha.
•	Giúp lớp con thực hiện phương thức theo cách riêng.
3. Ví dụ
class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Tieng dong vat");
    }
}

class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Gau gau");
    }
}
Khi chạy:
Animal a = new Dog();
a.Sound();
Kết quả:
Gau gau
Vì đối tượng thực tế là Dog nên chương trình gọi phương thức Sound() của lớp con, dù biến a có kiểu Animal. Đây chính là tính đa hình.
4. Bảng so sánh
virtual	override
Khai báo ở lớp cha	Khai báo ở lớp con
Cho phép ghi đè	Thực hiện ghi đè
Có thể có thân hàm mặc định	Thay đổi cách thực hiện của lớp cha
Không bắt buộc lớp con ghi đè	Phải ghi đè phương thức virtual hoặc override phù hợp
Kết luận: virtual cho phép lớp con thay đổi cách hoạt động của phương thức, còn override là cách lớp con thực hiện sự thay đổi đó. Hai từ khóa này giúp C# triển khai tính đa hình.

Câu 4: Tại sao static không thể truy xuất thông qua Object Instance?
1. Khái niệm static
•	static là thành phần thuộc về lớp (Class), không thuộc về từng đối tượng.
•	Có thể truy xuất trực tiếp thông qua tên lớp mà không cần tạo đối tượng bằng new.
2. Tại sao không truy xuất qua Object?
Vì static dùng chung cho cả lớp, không gắn với một đối tượng cụ thể. Trong khi đó, Object Instance là một đối tượng riêng được tạo từ lớp.
3. Ví dụ
class Student
{
    public static string School = "ABC";
    public string Name = "Dat";
}

Student sv = new Student();

Console.WriteLine(Student.School); // Đúng
Console.WriteLine(sv.Name);        // Đúng

// Console.WriteLine(sv.School);   // Lỗi
4. Kết luận
static thuộc về lớp chứ không thuộc về đối tượng, nên phải truy xuất bằng tên lớp, ví dụ Student.School, thay vì sv.School.
Cách này giúp phân biệt rõ dữ liệu dùng chung của cả lớp với dữ liệu riêng của từng đối tượng.

