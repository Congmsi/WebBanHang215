# CongShop Admin Layout - Hướng dẫn sử dụng

## Tổng quan

Layout mới này được thiết kế dành riêng cho hệ thống quản trị CongShop với những ưu điểm:

- **Sạch sẽ và hiện đại**: Thiết kế tối giản, dễ nhìn
- **Dễ tùy chỉnh**: CSS được tổ chức rõ ràng với CSS variables
- **Responsive**: Hoạt động tốt trên mobile và desktop
- **Hiệu suất cao**: Chỉ sử dụng Bootstrap và Font Awesome, không có dependency phức tạp

## Cách sử dụng

### 1. Sử dụng layout mới trong view

```razor
@{
    ViewData["Title"] = "Tên trang";
    Layout = "~/Areas/admin/Views/_LayoutCustomAdmin.cshtml";
}

<!-- Nội dung trang của bạn -->
```

### 2. Cấu trúc page header

```html
<div class="page-header">
    <h1 class="page-title">Tiêu đề trang</h1>
    <p class="page-subtitle">Mô tả ngắn về trang này</p>
</div>
```

### 3. Sử dụng cards

```html
<div class="admin-card">
    <div class="admin-card-header">
        <h5 class="mb-0">Tiêu đề card</h5>
    </div>
    <div class="admin-card-body">
        <!-- Nội dung card -->
    </div>
</div>
```

### 4. Statistics cards

```html
<div class="col-lg-3 col-md-6 mb-4">
    <div class="admin-card">
        <div class="admin-card-body text-center">
            <div class="d-flex align-items-center justify-content-between">
                <div>
                    <h3 class="text-primary mb-1">125</h3>
                    <p class="text-muted mb-0">Mô tả số liệu</p>
                </div>
                <div class="text-primary">
                    <i class="fas fa-box fa-2x"></i>
                </div>
            </div>
        </div>
    </div>
</div>
```

## Tùy chỉnh màu sắc

Bạn có thể dễ dàng thay đổi màu sắc bằng cách chỉnh sửa CSS variables trong file layout:

```css
:root {
    --primary-color: #2563eb;        /* Màu chính */
    --primary-dark: #1d4ed8;         /* Màu chính đậm */
    --secondary-color: #64748b;      /* Màu phụ */
    --success-color: #059669;        /* Màu thành công */
    --warning-color: #d97706;        /* Màu cảnh báo */
    --danger-color: #dc2626;         /* Màu nguy hiểm */
    --sidebar-bg: #1e293b;           /* Màu nền sidebar */
    --content-bg: #f8fafc;           /* Màu nền content */
}
```

## Thêm menu mới

Để thêm menu mới vào sidebar, chỉnh sửa phần `sidebar-nav` trong layout:

```html
<div class="nav-section">
    <div class="nav-section-title">Tên nhóm menu</div>
    <div class="nav-item">
        <a href="@Url.Action("Action", "Controller", new { area = "admin" })" class="nav-link">
            <i class="fas fa-icon"></i>
            <span>Tên menu</span>
        </a>
    </div>
</div>
```

Hoặc để tạo submenu:

```html
<div class="nav-item">
    <a href="#" class="nav-link" data-bs-toggle="collapse" data-bs-target="#menuId" aria-expanded="false">
        <i class="fas fa-icon"></i>
        <span>Menu cha</span>
        <i class="fas fa-chevron-right nav-toggle"></i>
    </a>
    <div class="collapse nav-submenu" id="menuId">
        <a href="#" class="nav-link">
            <span>Menu con 1</span>
        </a>
        <a href="#" class="nav-link">
            <span>Menu con 2</span>
        </a>
    </div>
</div>
```

## Utility Classes có sẵn

- `.text-primary`, `.text-success`, `.text-warning`, `.text-danger` - Màu chữ
- `.admin-card` - Card container
- `.admin-card-header` - Header của card
- `.admin-card-body` - Body của card
- `.page-header` - Header của trang
- `.page-title` - Tiêu đề trang
- `.page-subtitle` - Phụ đề trang

## Responsive Design

Layout tự động thích ứng với mobile:
- Trên mobile: Sidebar ẩn và có thể mở bằng nút menu
- Desktop: Sidebar cố định bên trái
- Tất cả components đều responsive

## Scripts tùy chỉnh

Để thêm JavaScript tùy chỉnh cho trang, sử dụng section Scripts:

```razor
@section Scripts {
    <script>
        // JavaScript tùy chỉnh cho trang này
    </script>
}
```

## Ví dụ trang hoàn chỉnh

Xem file `IndexCustom.cshtml` để biết cách sử dụng đầy đủ các component của layout.
