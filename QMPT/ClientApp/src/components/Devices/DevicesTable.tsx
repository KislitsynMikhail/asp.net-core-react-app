import React from "react";
import Paper from "@material-ui/core/Paper";
import TableContainer from "@material-ui/core/TableContainer";
import Table from "@material-ui/core/Table";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TableCell from "@material-ui/core/TableCell";
import TableBody from "@material-ui/core/TableBody";
import TablePagination from "@material-ui/core/TablePagination";
import {makeStyles} from "@material-ui/core";
import {Device} from "../../redux/reducers/devices-reducer";

export type ColumnId = 'number' | 'measurementRange' | 'permissibleSystematicErrorMax' | 'admissibleRandomErrorsMax'
    | 'magnetometerReadingsVariation' | 'gradientResistance' | 'signalAmplitude' | 'relaxationTime' | 'optimalCycle'

export interface Column {
    id: ColumnId
    label: string;
    minWidth?: number;
    align?: 'right';
    format?: (value: number) => string;
}

export const columns: Array<Column> = [
    { id: 'number', label: 'Номер', minWidth: 40 },
    { id: 'measurementRange', label: 'Диапазон измерения', minWidth: 40 },
    { id: 'permissibleSystematicErrorMax', label: 'Предел допускаемой систематической погрешности', minWidth: 100 },
    { id: 'admissibleRandomErrorsMax', label: 'Предел допускаемой случайной погрешности при цикле измерения', minWidth: 100 },
    { id: 'magnetometerReadingsVariation', label: 'Изменение показаний магнитометра при изменении напряжения питания в диапазоне 10 – 15В', minWidth: 100 },
    { id: 'gradientResistance', label: 'Градиентоустойчивость', minWidth: 100 },
    { id: 'signalAmplitude', label: 'Амплитуда сигнала', minWidth: 50 },
    { id: 'relaxationTime', label: 'Время релаксации', minWidth: 50 },
    { id: 'optimalCycle', label: 'Оптимальный цикл', minWidth: 40 }
]

const useStyles = makeStyles({
    root: {
        width: '100%',
        display: "flex",
        justifyContent: "center",
        flexDirection: "column"
    },
    container: {
        maxHeight: '100%',
    },
    innerCell: {
        borderBottom: 0
    },
    div: {
        width: '95%',
        marginLeft: '2.5%',
        display: "flex",
        alignItems: "flex-start"
    },
    row: {
        cursor: "pointer"
    }
})

type Props = {
    page: number
    setPage: (page: number) => void
    rowsPerPage: number
    setRowsPerPage: (rowsPerPage: number) => void
    devices: Array<Device>
    count: number
    onRowClick: (id: number) => void
}

const DevicesTable: React.FC<Props> = ({page, devices, onRowClick, setPage, setRowsPerPage, count, rowsPerPage}) => {
    const classes = useStyles()
    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage)
    }
    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(+event.target.value)
        setPage(0)
    }
    return (
        <div className={classes.div}>
            <Paper className={classes.root}>
                <TableContainer className={classes.container}>
                    <Table stickyHeader aria-label="sticky table">
                        <TableHead>
                            <TableRow>
                                <TableCell key={'number'} align={"center"}>
                                    Номер
                                </TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {devices.map((device) => {
                                return (
                                    <TableRow
                                        hover
                                        role="checkbox"
                                        tabIndex={-1}
                                        key={device.id}
                                        onClick={() => onRowClick(device.id)}
                                        className={classes.row}
                                    >
                                        <TableCell>
                                            {device.number}
                                        </TableCell>
                                    </TableRow>
                                );
                            })}
                        </TableBody>
                    </Table>
                </TableContainer>
                <TablePagination
                    labelDisplayedRows={(args) => (`${args.from}-${args.to} из ${args.count !== -1 ? args.count : ''}`)}
                    labelRowsPerPage={'Строк на странице'}
                    rowsPerPageOptions={[10, 25, 100]}
                    component="div"
                    count={count}
                    rowsPerPage={rowsPerPage}
                    page={page}
                    onChangePage={handleChangePage}
                    onChangeRowsPerPage={handleChangeRowsPerPage}
                />
            </Paper>
        </div>
    )
}

export default DevicesTable