import { ReactNode } from "react";
import { useNavigate } from "react-router-dom";

import { Table as MuiTable, Typography } from "@mui/material";
import Paper from "@mui/material/Paper";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";

import { Entity } from "../types";

export type CellAlign = "center" | "left" | "right" | "inherit" | "justify";

function getValue<T extends unknown>(obj: T, key: keyof T) {
  return obj[key];
}

export interface Column<T> {
  id: string;
  label: string;
  minWidth?: number;
  align?: CellAlign;
  format?: (value: T) => string | ReactNode;
}

interface TableProps<T> {
  columns: Column<T>[];
  data: T[] | undefined;
  navigateRoute?: string;
  page: number;
  itemsPerPage: number;
  handleChangePage: (event: unknown, newPage: number) => void;
  handleChangeItemsPerPage: (
    event: React.ChangeEvent<HTMLInputElement>
  ) => void;
  totalCount: number | undefined;
  title?: string;
}

const Table = <T extends Entity>({
  columns,
  data = [],
  navigateRoute,
  page,
  itemsPerPage,
  handleChangePage,
  handleChangeItemsPerPage,
  totalCount = 0,
  title,
}: TableProps<T>) => {
  const navigate = useNavigate();

  return (
    <Paper sx={{ width: "100%", overflow: "hidden" }}>
      {title && (
        <Typography
          sx={{ flex: "1 1 100%", p: 2, backgroundColor: "#ededed" }}
          variant="h6"
          id="tableTitle"
          component="div"
        >
          {title}
        </Typography>
      )}
      <TableContainer
        sx={{
          maxHeight: "calc(100vh - 215px)",
          minHeight: "calc(100vh - 215px)",
        }}
      >
        <MuiTable stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              <TableCell></TableCell>
              {columns.map((column: Column<T>) => (
                <TableCell
                  key={column.id}
                  align={column.align}
                  style={{ minWidth: column.minWidth }}
                >
                  {column.label}
                </TableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {data.map((row: T, index) => (
              <TableRow
                key={row.id}
                hover
                role="checkbox"
                tabIndex={-1}
                sx={{ cursor: navigateRoute ? "pointer" : "default" }}
                onClick={() =>
                  navigateRoute
                    ? navigate(`${navigateRoute}/${row.id}`)
                    : undefined
                }
              >
                <TableCell>
                  #{" "}
                  {page > 1 ? (page - 1) * itemsPerPage + index + 1 : index + 1}
                </TableCell>
                {columns.map((column: Column<T>) => {
                  const value = getValue(row, column.id as keyof T);
                  return (
                    <TableCell key={column.id} align={column.align}>
                      {column.format ? column.format(row) : (value as string)}
                    </TableCell>
                  );
                })}
              </TableRow>
            ))}
          </TableBody>
        </MuiTable>
      </TableContainer>

      <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component="div"
        count={totalCount}
        rowsPerPage={itemsPerPage}
        page={page - 1}
        onPageChange={(event, page) => handleChangePage(event, page + 1)}
        onRowsPerPageChange={handleChangeItemsPerPage}
        labelRowsPerPage={"Rows"}
      />
    </Paper>
  );
};

export default Table;
