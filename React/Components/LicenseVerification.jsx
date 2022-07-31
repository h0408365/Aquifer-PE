import React, { useState } from 'react'
import { Table, Button, Form } from 'react-bootstrap'
import FileUploader from '../../components/fileUploader/FileUploader'
import './licenseverification.css'
import { MdOutlineKeyboardArrowDown } from 'react-icons/md';
import * as licenseVerificationService from '../../services/licenseVerificationService'

import debug from 'sabio-debug';
const _logger = debug.extend('UserLocationVerification');

function LicenseVerification() {
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
        licenseVerificationService
            .selectbyUserLicenseId(12)
            .then(onGetUserLicenseSuccess)
            .catch(onGetUserLicenseError);
        if (userLicenseSelected === false) {
            updateUserLicenseSelected(!userLicenseSelected);
        }
        else updateUserLicenseSelected(!userLicenseSelected);

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

    const onLicenseTypeClicked = (e) => {
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
        debugger;
        currentRecord[licenseTypeForUpload] = data.items[0].url;
        debugger;
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
            <div className="container" >
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

                <div id="button-upload" className="text-center"
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
                                    <b> Thank you for applying to join CNM Pro - you are almost there! There are a few steps
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
                                    3. Please select one of the license types from the dropdown menu. Then click the
                                    file uploader where indicated. Or click and drag that file where indicated.
                                </div>
                                <div>
                                    4. Confirm that your license has populated in the table. The next process is that we have to
                                    verify your license so employers can reach out to you. <b>Thank you for your patience</b>.
                                </div>
                            </div>
                            <div className="dropdown-file-type py-1 col-md-8 offset-md-2">
                                <Form.Select aria-label="Default select example" onChange={onLicenseTypeClicked}>
                                    <option>Please select a license to upload</option>
                                    <option value="url">Civil Engineer</option>
                                    <option value="url">Structural Engineer</option>
                                    <option value="url">Geotechnical Engineer</option>
                                    <option value="url">Mechanical Engineer</option>
                                    <option value="url">Electrical Engineer</option>
                                    <option value="url">Architect Engineer</option>
                                    <option value="url">Landscape Engineer</option>
                                    <option value="url">Traffic Engineer</option>
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
                                            <th>#</th>
                                            <th className="text-center" >License</th>
                                        </tr>
                                    </thead>
                                    <tbody>
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

export default LicenseVerification
