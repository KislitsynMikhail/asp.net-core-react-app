import React from "react";
import Paper from "@material-ui/core/Paper";
import TableContainer from "@material-ui/core/TableContainer";
import Table from "@material-ui/core/Table";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TableCell from "@material-ui/core/TableCell";
import TableBody from "@material-ui/core/TableBody";
import TablePagination from "@material-ui/core/TablePagination";
import {makeStyles} from "@material-ui/core/styles";
import { Price } from "../../redux/reducers/prices-reducer";

export interface PriceTableColumn {
    id: PriceTableColumnId
    label: string;
    minWidth?: number;
    align?: 'right';
    format?: (value: number) => string;
}

export type PriceTableColumnId = 'name' | 'cost'

export const columns: PriceTableColumn[] = [
    { id: 'name', label: 'Название', minWidth: 100 },
    { id: 'cost', label: 'Стоимость', minWidth: 100 }
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
    div: {
        width: '47.5%',
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
    prices: Array<Price>
    count: number
    onRowClick: (priceId: number) => void
}

const PriceTable: React.FC<Props> = ({
                                         page,
                                         setPage,
                                         rowsPerPage,
                                         setRowsPerPage,
                                         prices,
                                         count,
                                         onRowClick}) => {
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
                                {columns.map((column) => (
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
                            { prices.map((price) => {
                                    return (
                                        <TableRow
                                            hover
                                            role="checkbox"
                                            tabIndex={-1}
                                            key={price.id}
                                            onClick={() => onRowClick(price.id)}
                                            className={classes.row}
                                        >
                                            { columns.map((column) => {
                                                return (
                                                    <TableCell key={column.id} align={column.align}>
                                                        {price[column.id]}
                                                    </TableCell>
                                                );
                                            }) }
                                        </TableRow>
                                    );
                                }) }
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

export default PriceTable