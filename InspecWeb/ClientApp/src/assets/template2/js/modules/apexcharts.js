import ApexCharts from "apexcharts";

window.ApexCharts = ApexCharts;

$(function() {
  window.Apex = {
    colors: [
      window.theme.primary,
      window.theme.success,
      window.theme.warning,
      window.theme.danger,
      window.theme.info
    ]
  };
});
