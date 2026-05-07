    .global entry
    .global ulp_water_threshold
    .global ulp_water_value

    .data
    .align 4
ulp_water_threshold:
    .long 500
ulp_water_value:
    .long 0

    .text
    .global entry
entry:
    /* Измеряем ADC на GPIO35 (ADC1_CH7) */
    WRITE_RTC_REG(SENS_SAR_MEAS1_CTRL2_REG, 14, 7, 0)
    WRITE_RTC_REG(SENS_SAR_MEAS1_CTRL2_REG, 11, 3, 0)
    WRITE_RTC_REG(SENS_SAR_MEAS1_MUX_REG, 0, 5, 7)
    WRITE_RTC_REG(SENS_SAR_MEAS1_CTRL2_REG, 2, 3, 0)
    WRITE_RTC_REG(SENS_SAR_MEAS1_CTRL2_REG, 5, 3, 1)

wait_adc:
    READ_RTC_REG(SENS_SAR_MEAS1_CTRL2_REG, 0, 8)
    and r0, r0, 0xff
    jump wait_adc, r0, 0, lt

    READ_RTC_REG(SENS_SAR_MEAS1_CTRL2_REG, 16, 12)
    move r1, r0

    move r2, ulp_water_value
    st r1, r2, 0

    move r3, ulp_water_threshold
    ld r3, r3, 0
    sub r0, r1, r3
    jump wakeup, r0, 0, gt
    halt

wakeup:
    WRITE_RTC_REG(RTC_CNTL_STATE0_REG, RTC_CNTL_ULP_CP_WAKEUP_FORCE, 1, 1)
    halt
