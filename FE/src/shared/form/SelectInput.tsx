import React, { FC } from "react";
import { useFormContext } from "react-hook-form";

import {
  FormControl,
  FormHelperText,
  InputLabel,
  OutlinedInput,
  Select,
} from "@mui/material";

export interface Option {
  label: string;
  value: string | number;
}

interface SelectInputProps {
  field: string;
  label: string;
  options: Option[];
  disabled?: boolean;
}

const SelectInput: FC<SelectInputProps> = ({
  field,
  label,
  options,
  disabled = false,
}) => {
  const {
    register,
    formState: { errors },
    getValues,
  } = useFormContext();

  return (
    <FormControl size="small">
      <InputLabel htmlFor={field}>{label}</InputLabel>
      <Select
        {...register(field)}
        input={
          <OutlinedInput id="select-multiple-chip" label={label} notched />
        }
        native
        defaultValue={getValues(field)}
        id={field}
        aria-describedby={`${field}-helper-text`}
        disabled={disabled}
      >
        {options.map(({ label, value }) => (
          <option key={value} value={value}>
            {label}
          </option>
        ))}
      </Select>
      {errors[field] && (
        <FormHelperText id={`${field}-helper-text`} sx={{ color: "red" }}>
          {errors[field]?.message?.toString()}
        </FormHelperText>
      )}
    </FormControl>
  );
};

export default SelectInput;
