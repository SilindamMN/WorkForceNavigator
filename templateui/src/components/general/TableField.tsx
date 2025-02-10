import React, { useState } from "react";
import { Table, ButtonGroup } from "react-bootstrap";
import { Button, TableHead, TablePagination } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteOutlineIcon from "@mui/icons-material/DeleteOutline";
import { format } from "date-fns";

interface TableFieldProps<T> {
  rows: T[];
  columns: { key: string; label: string }[];
  onEdit?: (data: any) => void;
  onDelete?: (id: number) => void;
  options?: { value: any; label: string }[];
  onRowClick?: (value: any) => void; // Added this line
  showActions?: boolean;
}

const TableField = <T,>({
  rows,
  columns,
  onEdit,
  onDelete,
  onRowClick, // Add this line,
  showActions,
}: TableFieldProps<T>) => {
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const slicedRows = rows.slice(
    page * rowsPerPage,
    page * rowsPerPage + rowsPerPage
  );

  const formatDate = (date: Date) => {
    try {
      return format(date, "yyyy-MM-dd");
    } catch (error) {
      console.error("Invalid date format", date);
      return date.toString();
    }
  };

  return (
    <div className="px-4 my-2 w-full">
      <Table striped bordered hover>
        <thead>
          <tr>
            {columns.map((column) => (
              <th key={column.key}>{column.label}</th>
            ))}{" "}
            {showActions && <th>Actions</th>}
          </tr>
        </thead>
        <tbody>
          {slicedRows.map((row: any, index) => (
            <tr key={index} onClick={() => onRowClick && onRowClick(row)}>
              {" "}
              {columns.map((column) => (
                <td key={column.key}>
                  {console.log(column.key)} 
                  {column.key === "date"
                    ? formatDate((row as any)[column.key])
                    : (row as any)[column.key]}
                </td>
              ))}
              <td>
                {showActions && (
                  <ButtonGroup>
                    <Button
                      variant="outlined"
                      color="warning"
                      sx={{ height: "30px", width: "50%" }} // Adjust the height as needed
                      style={{ fontSize: "medium" }}
                      startIcon={<EditIcon />}
                      onClick={() => onEdit && onEdit(row)} // Pass the row data to the edit function
                    >
                      Edit
                    </Button>
                    <Button
                      variant="outlined"
                      color="error"
                      sx={{ height: "30px" }} // Adjust the height as needed
                      style={{ fontSize: "medium" }}
                      startIcon={<DeleteOutlineIcon />}
                      onClick={() => onDelete && onDelete(row.id)} // Implement delete functionality if needed
                    >
                      Delete
                    </Button>
                  </ButtonGroup>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
      <TablePagination
        rowsPerPageOptions={[5, 10, 25]}
        component="div"
        count={rows.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />
    </div>
  );
};

export default TableField;