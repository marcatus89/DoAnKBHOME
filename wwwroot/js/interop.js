// File này chứa các hàm JavaScript để tương tác với thư viện NProgress

// Hàm ẩn thanh tiến trình
export function stopProgressBar() {
  NProgress.done();
}

// Hàm này thiết lập một trình lắng nghe sự kiện cho toàn bộ trang
// để tự động bắt đầu thanh tiến trình khi người dùng nhấp vào một link nội bộ
export function setupNavigationProgressListener() {
  document.body.addEventListener('click', function (event) {
    // Tìm thẻ <a> gần nhất với vị trí nhấp chuột
    const anchor = event.target.closest('a');

    // Kiểm tra xem đó có phải là một link điều hướng nội bộ của Blazor không
    if (anchor && anchor.href && anchor.target !== '_blank' && anchor.hasAttribute('href') && !anchor.getAttribute('href').startsWith('#')) {
      NProgress.start();
    }
  });
}
export function printPage() {
  window.print();
}
