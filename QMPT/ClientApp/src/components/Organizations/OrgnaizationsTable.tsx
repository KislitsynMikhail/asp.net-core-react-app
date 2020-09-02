import React from "react";
import { makeStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import {Organization} from "../../redux/reducers/organizations-reducer";

export interface OrganizationTableColumn {
    id: OrganizationTableColumnId
    label: string;
    minWidth?: number;
    align?: 'right';
    format?: (value: number) => string;
}

export type OrganizationTableColumnId = 'name' | 'inn' | 'kpp' | 'ogrn' | 'okpo' | 'legalAddress' | 'email' | 'phoneNumber' | 'settlementAccount' |
    'corporateAccount' | 'bik' | 'managerPosition' | 'base' | 'supervisorFIO' | 'chiefAccountant' | 'isUsn'

export const columns: OrganizationTableColumn[] = [
    { id: 'name', label: 'Название', minWidth: 100 },
    { id: 'inn', label: 'ИНН', minWidth: 15 },
    { id: "kpp", label: 'КПП', minWidth: 15 },
    { id: "ogrn", label: 'ОГРН', minWidth: 15 },
    { id: "okpo", label: 'ОКПО', minWidth: 15 },
    { id: "legalAddress", label: 'Юридический адрес', minWidth: 100 },
    { id: "email", label: "Email", minWidth: 50 },
    { id: "phoneNumber", label: "Телефон", minWidth: 15 },
    { id: "settlementAccount", label: 'Рассчетный счет', minWidth: 30 },
    { id: "corporateAccount", label: 'Корпоративный счет', minWidth: 30 },
    { id: "bik", label: "БИК", minWidth: 10 },
    { id: "managerPosition", label: "Должность руководителя", minWidth: 50 },
    { id: "base", label: "Основание", minWidth: 100 },
    { id: "supervisorFIO", label: "ФИО руководителя", minWidth: 100 },
    { id: "chiefAccountant", label: "Главный бухгалтер", minWidth: 100 },
    { id: "isUsn", label: "УСН", minWidth: 10 }
]

export type Data = {
    id: number
    name: string
    inn: string
    kpp: string
    ogrn: string
    okpo: string
    legalAddress: string
    email: string
    phoneNumber: string
    settlementAccount: string
    corporateAccount: string
    bik: string
    managerPosition: string
    base: string
    supervisorFIO: string
    chiefAccountant: string
    isUsn: string
}

const useStyles = makeStyles({
    root: {
        width: '95%',
        display: "flex",
        justifyContent: "center",
        flexDirection: "column"
    },
    container: {
        maxHeight: '100%',
    },
    div: {
        display: "flex",
        justifyContent: "center"
    },
    row: {
        cursor: "pointer"
    }
})

const getDataFromOrganizations = (organizations: Array<Organization>): Array<Data> => (
    organizations.map<Data>(organization => ({
        id: organization.id,
        name: organization.name,
        inn: organization.inn,
        kpp: organization.kpp,
        ogrn: organization.ogrn,
        okpo: organization.okpo,
        legalAddress: organization.legalAddress,
        email: organization.email,
        phoneNumber: organization.phoneNumber,
        settlementAccount: organization.settlementAccount,
        corporateAccount: organization.corporateAccount,
        bik: organization.bik,
        managerPosition: organization.managerPosition,
        base: organization.base,
        supervisorFIO: organization.supervisorFIO,
        chiefAccountant: organization.chiefAccountant,
        isUsn: organization.isUsn ? 'Да' : 'Нет'
    }))
)


type Props = {
    showContacts: (organizationId: number) => void
    page: number
    setPage: (page: number) => void
    rowsPerPage: number
    setRowsPerPage: (rowsPerPage: number) => void,
    organizations: Array<Organization>,
    count: number
}

const OrganizationTable: React.FC<Props> = ({
                                                showContacts,
                                                page,
                                                setPage,
                                                rowsPerPage,
                                                setRowsPerPage,
                                                organizations,
                                                count}) => {
    const classes = useStyles()
    const rows = getDataFromOrganizations(organizations)

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage)
    }

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(+event.target.value)
        setPage(0)
    }

    const onRowClick = (id: number) => {
        showContacts(id)
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
                                {rows.map((row) => {
                                    return (
                                        <TableRow hover role="checkbox" tabIndex={-1} key={row.id} onClick={() => onRowClick(row.id)} className={classes.row}>
                                            {columns.map((column) => {
                                                return (
                                                    <TableCell key={column.id} align={column.align}>
                                                        {row[column.id]}
                                                    </TableCell>
                                                );
                                            })}
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

export default OrganizationTable