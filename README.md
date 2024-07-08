# Project test việc sử dụng 2 kiểu dữ liệu là ConcurrentQueue và List khi làm việc đa luồng (mutithread) với ngôn ngữ lập trình C#

Kiểu dữ liệu List:

```
Nếu làm việc với kiểu dữ liệu này, cần đảm bảo rằng List phải được lock khi thêm mới hoặc remove 1 phần tử khỏi list. Kiểm soát tất cả các chỗ gọi lock 1 cách chặt chẽ
```

Kiểu dữ liệu ConcurrentQueue:

```
C# cung cấp sẵn kiểu dữ liệu này để tự lock khi cần thiết nếu làm việc đa luồng chung 1 object rồi.
```
