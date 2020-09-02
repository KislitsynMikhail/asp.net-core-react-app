import React, {useState} from 'react';
import {connect} from "react-redux";
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import {makeStyles} from "@material-ui/core/styles";
import OrganizationAddEditForm, {AddFormData} from "./OrganizationAddEditForm";
import ContactPersons from './ContactPersons';
import {
    Organization,
    editOrganization,
    deleteOrganization
} from "../../redux/reducers/organizations-reducer"


const useStyles = makeStyles({
    editForm: {
        marginTop: 20
    }
});

type Props = {
    organization: Organization
    isOpen: boolean
    closeDialog: () => void
} & MapDispatchToProps

const OrganizationDialog: React.FC<Props> = ({
                                                 organization,
                                                 isOpen,
                                                 closeDialog,
                                                 editOrganization,
                                                 deleteOrganization}) => {
    const classes = useStyles()
    const [isEditMode, setIsEditMode] = useState(false)

    const descriptionElementRef = React.useRef<HTMLElement>(null);
    React.useEffect(() => {
        if (isOpen) {
            const { current: descriptionElement } = descriptionElementRef;
            if (descriptionElement !== null) {
                descriptionElement.focus();
            }
        }
    }, [isOpen])

    const onEditOrSaveButtonClick = (organizationForm: AddFormData) => {
        if (isEditMode) {
            editOrganization({...organizationForm, organizationType: organization.organizationType})
            setIsEditMode(false)
        } else {
            setIsEditMode(true)
        }
    }

    const onCancelButtonClick = () => {
        if (!isEditMode) {
            deleteOrganization(organization.id)
            closeDialog()
        }
        setIsEditMode(false)
    }

    return (
        <div>
            <Dialog
                open={isOpen}
                onClose={closeDialog}
                scroll={'paper'}
                maxWidth={"lg"}
                aria-labelledby="scroll-dialog-title"
                aria-describedby="scroll-dialog-description"
            >
                <DialogContent dividers={true}>
                    <div className={classes.editForm}>
                        <OrganizationAddEditForm
                            onEditOrAddButtonClick={onEditOrSaveButtonClick}
                            onCancelButtonClick={onCancelButtonClick}
                            buttonNameOnEditOrAddingMode={'Сохранить'}
                            initialValues={organization}
                            isEditOrAddingMode={isEditMode}
                        />
                    </div>
                    <ContactPersons
                        organizationId={organization.id}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={closeDialog} color="primary">
                        Ok
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}

type MapDispatchToProps = {
    editOrganization: (organization: Organization) => void
    deleteOrganization: (organizationId: number) => void
}
export default connect(null,
    {
        editOrganization,
        deleteOrganization
    })
(OrganizationDialog)