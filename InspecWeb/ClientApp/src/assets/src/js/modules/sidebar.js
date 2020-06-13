$(function() {
  /* Sidebar toggle behaviour */
  $(".sidebar-toggle").on("click touch", function() {
    $(".sidebar").toggleClass("toggled");
  });

  // const active = $(".sidebar .active");
  // console.log('sidebar',active);
  
  // if (active.length && active.parent(".collapse").length) {
  //   const parent = active.parent(".collapse");

  //   parent.prev("a").attr("aria-expanded", true);
  //   parent.addClass("show");
  // }
});
