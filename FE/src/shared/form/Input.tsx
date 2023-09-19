import React, { FC } from "react";
import { useFormContext } from "react-hook-form";

import {
  FormControl,
  FormHelperText,
  InputLabel,
  OutlinedInput as MUIInput,
  SxProps,
} from "@mui/material";

interface InputProps {
  field: string;
  label?: string;
  sx?: SxProps;
  type?: "password" | "number" | "file";
  accept?: string;
  onChange?: (
    event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>
  ) => void;
  multiline?: boolean;
  maxRows?: number;
  rows?: number;
  fullWidth?: boolean;
}

const Input: FC<InputProps> = ({
  field,
  label,
  type = "text",
  accept,
  onChange,
  multiline,
  maxRows,
  rows,
  sx,
  fullWidth = true,
}) => {
  const {
    register,
    formState: { errors },
  } = useFormContext();

  return (
    <FormControl fullWidth={fullWidth}>
      <InputLabel htmlFor={field} size={"small"}>
        {label}
      </InputLabel>
      <MUIInput
        {...register(field, { ...(onChange && { onChange: onChange }) })}
        id={field}
        type={type}
        aria-describedby={`${field}-helper-text`}
        inputProps={{ accept: accept }}
        multiline={multiline}
        maxRows={maxRows}
        rows={rows}
        label={label}
        sx={sx}
        size={"small"}
      />
      {errors[field] && (
        <FormHelperText id={`${field}-helper-text`} sx={{ color: "red" }}>
          {errors[field]?.message?.toString()}
        </FormHelperText>
      )}
    </FormControl>
  );
};

export default Input;
