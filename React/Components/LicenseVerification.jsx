import React, { useState } from 'react'
import { Table, Button, Form } from 'react-bootstrap'
import FileUploader from '../../components/fileUploader/FileUploader'
import './LicenseVerification.css'
import { MdOutlineKeyboardArrowDown } from 'react-icons/md';
import * as licenseVerificationService from '../../services/licenseVerificationService'
import PropTypes from 'prop-types';


import debug from 'sabio-debug';
const _logger = debug.extend('UserLocationVerification');

function LicenseVerification(props) {
    const [userLicenseSelected, updateUserLicenseSelected] = useState(false);
    const [licenseTypeSelected, updateLicenseTypeSelected] = useState(false);
    const [licenseTypeForUpload, updateLicenseTypeForUpload] = useState({});

    const [formData, setFormData] = useState({
        id: ''
        , licenseTypesId: ''
        , userId: ''
        , locationsId: ''
        , licenseStateId: ''
        , url: ''
        , dateExpires: ''
    })

    const onUserClicked = () => {
        licenseVerificationService.selectbyUserLicenseId(props.currentUser.id)
            .then(onGetUserLicenseSuccess)
            .catch(onGetUserLicenseError);
    }

    const onGetUserLicenseSuccess = (data) => {
        setFormData(() => {
            let currentRecord = { ...data.item };
            if (userLicenseSelected === false) {
                updateUserLicenseSelected(!userLicenseSelected)
            }
            return currentRecord
        })
    }

    const onClickToUpload = (e) => {
        if (e.target.value !== 'Please select a license to upload') {
            updateLicenseTypeForUpload(e.target.value);
            if (licenseTypeSelected === false) {
                updateLicenseTypeSelected(!licenseTypeSelected);
            }
            return licenseTypeForUpload;
        } else updateLicenseTypeSelected(!licenseTypeSelected)
    }

    const onHandleUploadSuccess = (data) => {
        _logger('File Upload Success')
        const currentRecord = formData;
        currentRecord[licenseTypeForUpload] = data.items[0].url;
        licenseVerificationService
            .update(currentRecord, formData.id)
            .then(onRecordUpdateSuccess)
            .catch(onRecordUpdateError);
    }

    const onRecordUpdateSuccess = () => {
        _logger('License verification record update successful')
        licenseVerificationService
            .selectbyUserLicenseId(formData.id)
            .then(onGetUserLicenseSuccess)
            .catch(onGetUserLicenseError);
    }
    const onRecordUpdateError = (error) => {
        _logger("An error occured locating this user's location verification records", error);
        return error;
    };

    const onGetUserLicenseError = (error) => {
        _logger("An error occured locating this user's location verification records", error);
        return error;
    };
    return (
        <React.Fragment>
            <div id="license-verification-heading-wrapper" >
                <div className="row">
                    <h1 className="container text-center header-font" style={{ marginTop: "130px", marginBottom: "5px" }}>
                        Verification
                    </h1>
                </div>

                <div className="body-font text-center" style={{ marginBottom: "10px" }}>
                    <label className="body-font" >
                        Please Click the Button Below to Start or Resume the License Verification Process
                    </label>
                </div>

                <div id="capture-user-button-upload" className="text-center"
                    style={{ marginBottom: "20px" }}>
                    <Button className="button" onClick={onUserClicked}>
                        <MdOutlineKeyboardArrowDown size="33px" /> Click here to continue with Verification
                    </Button>
                </div>
                <div className="body-font" >
                    {userLicenseSelected && (
                        <>
                            <div className="col-md-8 offset-md-2">
                                <div className="py-1">
                                    {' '}
                                    <b> Thank you for applying to join Aquifer PE - you are almost there! There are a few steps
                                        you need to follow: </b>
                                </div>
                                <div className="py-1">
                                    1. Please confirm that the information below in the table is correctly populated. If it
                                    is not, please reach out to us through the help form.
                                </div>
                                <div className="py-1">
                                    2. You will need to submit a license with the following information on it:{' '}
                                    <b>License Number</b>,{' '}
                                    <b>the state that the license was acquired in</b>, and the{' '}
                                    <b>expiration date of the license</b>.
                                </div>
                                <div>
                                    3. Please select one of the document types from the dropdown menu. Then click the
                                    file uploader where indicated. Or click and drag that file where indicated.
                                </div>
                                <div>
                                    4. Confirm that your document has populated in the table. The next process is that we have to
                                    verify your document so employers can reach out to you. <b>Thank you for your patience</b>.
                                </div>
                            </div>
                            <div className="dropdown-file-type py-1 col-md-8 offset-md-2">
                                <Form.Select aria-label="Default select example" onChange={onClickToUpload}>
                                    <option>Please select document to upload</option>
                                    <option value="url">License</option>
                                </Form.Select>
                            </div>
                            <div className='col-md-8 offset-md-2' >
                                {licenseTypeSelected && (
                                    <FileUploader
                                        className='col-md-6'
                                        onHandleUploadSuccess={onHandleUploadSuccess}
                                    />
                                )}
                            </div>
                            <div className="col-md-8 offset-md-2">
                                < Table striped bordered hover size="sm" style={{ paddingTop: '50px' }}>
                                    <thead>
                                        <tr>
                                            <th className="text-center">#</th>
                                            <th className="text-center" >License</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td className="text-center">1</td>
                                            <td className="text-center">
                                                <a
                                                    href={formData.url}
                                                    target="_blank"
                                                    rel="noopener noreferrer">
                                                    {formData.url}
                                                </a>
                                            </td>
                                        </tr>
                                    </tbody>
                                </Table>
                            </div>
                        </>
                    )}
                </div>
            </div >

        </React.Fragment >
    )
}

LicenseVerification.propTypes = {
    currentUser: PropTypes.shape({
        id: PropTypes.number,
        roles: PropTypes.arrayOf(PropTypes.string),
        email: PropTypes.string,
        isLoggedIn: PropTypes.bool.isRequired,
    }),
};

export default LicenseVerification
