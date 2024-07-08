# Project test việc sử dụng 2 kiểu dữ liệu là ConcurrentQueue và List khi làm việc đa luồng (mutithread) với ngôn ngữ lập trình C#

Kiểu dữ liệu List:

```
Nếu làm việc với kiểu dữ liệu này, cần đảm bảo rằng List phải được lock khi thêm mới hoặc remove 1 phần tử khỏi list. Kiểm soát tất cả các chỗ gọi lock 1 cách chặt chẽ
```

Kiểu dữ liệu ConcurrentQueue:

```
C# cung cấp sẵn kiểu dữ liệu này để tự lock khi cần thiết nếu làm việc đa luồng chung 1 object rồi.
```

# Hướng dẫn chạy project

chạy 1 trong các project để test được từng kiểu dữ liệu

project sẽ test việc main thread liên tục thêm 1 element vào trong List/ConcurrentQueue

và thread khác sẽ liên tục đọc 1 element trong List/ConcurrentQueue để Console.WriteLine ra

Làm theo hướng này sẽ tránh bị phình ram