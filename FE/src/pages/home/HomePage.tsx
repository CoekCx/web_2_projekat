import React from "react";

import { Box, Paper, Typography } from "@mui/material";
import {
  DefaultizedPieValueType,
  pieArcLabelClasses,
  PieChart,
} from "@mui/x-charts";

import ShopChart from "../../components/chart/ShopChart";
import ShopLineChart from "../../components/chart/ShopLineChart";
import { drawerWidth } from "../../components/dashboard";

const sizing = {
  margin: { right: 5 },
  width: 200,
  height: 200,
  legend: { hidden: true },
};

const data = [
  { label: "Group A", value: 400, color: "#0088FE" },
  { label: "Group B", value: 300, color: "#00C49F" },
  { label: "Group C", value: 300, color: "#FFBB28" },
  { label: "Group D", value: 200, color: "#FF8042" },
];

const TOTAL = data.map((item) => item.value).reduce((a, b) => a + b, 0);

const getArcLabel = (params: DefaultizedPieValueType) => {
  const percent = params.value / TOTAL;
  return `${(percent * 100).toFixed(0)}%`;
};

export default function HomePage() {
  return (
    <Box
      sx={{
        position: "relative",
        height: "87vh",
        width: { sm: `calc(100vw - ${drawerWidth}px - 50px)` },
      }}
    >
      <Paper
        sx={{
          height: "87vh",
          width: { sm: `calc(100vw - ${drawerWidth}px - 50px)` },
          position: "absolute",
          left: 0,
          top: 0,
          backgroundImage:
            "url(https://static.independent.co.uk/s3fs-public/thumbnails/image/2017/12/19/15/istock-463173435.jpg)",
          backgroundPosition: "center",
          backgroundSize: "cover",
          backgroundRepeat: "no-repeat",
          opacity: 0.2,
        }}
      ></Paper>
      <Typography variant={"h5"} sx={{ p: 2 }}>
        Register and Shop with us!
      </Typography>
      <Typography variant={"body1"} sx={{ pl: 2 }}>
        Our Users and Sales throughout the Years
      </Typography>

      <Box
        sx={{
          position: "absolute",
          top: "50%",
          left: "50%",
          transform: "translate(-50%, -50%)",
        }}
      >
        <ShopChart />
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-around",
            alignItems: "center",
          }}
        >
          <ShopLineChart />
          <PieChart
            series={[
              {
                outerRadius: 80,
                data,
                arcLabel: getArcLabel,
              },
            ]}
            sx={{
              [`& .${pieArcLabelClasses.root}`]: {
                fill: "white",
                fontSize: 14,
              },
            }}
            {...sizing}
          />
        </Box>
      </Box>
    </Box>
  );
}
