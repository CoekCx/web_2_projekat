import * as React from "react";

import { LineChart } from "@mui/x-charts/LineChart";

const uData = [4000, 3000, 2000, 2780, 1890, 2390, 3490];
const xLabels = [
  "Page A",
  "Page B",
  "Page C",
  "Page D",
  "Page E",
  "Page F",
  "Page G",
];

export default function ShopLineChart() {
  return (
    <LineChart
      width={500}
      height={300}
      colors={["#cba31c"]}
      series={[{ data: uData, label: "sales", area: true }]}
      xAxis={[{ scaleType: "point", data: xLabels }]}
      sx={{
        ".MuiLineElement-root, .MuiMarkElement-root": {
          display: "none",
        },
      }}
    />
  );
}
