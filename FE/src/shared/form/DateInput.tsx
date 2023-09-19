import React, { FC, useEffect } from "react";
import dayjs from "dayjs";
import { useFormContext } from "react-hook-form";

import { FormControl, FormHelperText } from "@mui/material";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { DemoContainer } from "@mui/x-date-pickers/internals/demo";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";

interface DateInputProps {
  field: string;
  label: string;
}

const DateInput: FC<DateInputProps> = ({ field, label }) => {
  const methods = useFormContext();
  const {
    watch,
    setValue,
    formState: { errors },
  } = methods;

  const value = watch(field) || dayjs();

  useEffect(() => {
    setValue(field, value);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <FormControl>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <DemoContainer components={["DatePicker", "DatePicker"]}>
          <DatePicker
            sx={{ width: "100%" }}
            label={label}
            value={value}
            onChange={(newValue) => setValue(field, newValue)}
            slotProps={{ textField: { size: "small" } }}
          />
        </DemoContainer>
      </LocalizationProvider>
      {errors[field] && (
        <FormHelperText id={`${field}-helper-text`} sx={{ color: "red" }}>
          {errors[field]?.message?.toString()}
        </FormHelperText>
      )}
    </FormControl>
  );
};

export default DateInput;
