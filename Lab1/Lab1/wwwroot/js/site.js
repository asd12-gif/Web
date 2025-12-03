document.addEventListener('DOMContentLoaded', () => {
    const body = document.body;
    const toggleButton = document.getElementById('btnToggle');

    toggleButton.addEventListener('click', () => {
        // Thêm/Xóa class CSS 'dark-theme' trên thẻ body
        body.classList.toggle('dark-theme');

        // Thay đổi chữ trên nút (tùy chọn)
        if (body.classList.contains('dark-theme')) {
            toggleButton.textContent = "Bật Chế Độ Sáng";
        } else {
            toggleButton.textContent = "Bật/Tắt Chế Độ Tối";
        }
    });
});