
export function initCarousel(selector) {
  var myCarousel = document.querySelector(selector);
  if (myCarousel) {
    var carousel = new bootstrap.Carousel(myCarousel, {
      interval: 5000, // Tự động chuyển slide sau mỗi 5 giây
      wrap: true
    });
  }
}
