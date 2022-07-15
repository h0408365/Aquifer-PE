import React, { useState, useEffect, useCallback } from 'react';
import * as listingVerificationService from '../../services/listingVerificationService';
import debug from 'sabio-debug';
import Pagination from 'rc-pagination';
import 'rc-pagination/assets/index.css';
import FileUploader from '../../components/fileuploader/FileUploader';
import { Form, Table } from 'react-bootstrap';
import ListVerCard from '../../components/listingverification/lvcardtest';

function ListingVerification() {
    const _logger = debug.extend('ListingVerification');
    //const navigate = useNavigate();
    const [listingSelected, setListingSelected] = useState(false);
    const [docuTypeSelected, setDocuTypeSelected] = useState(false);
    const [docuTypeUploader, setDocuTypeUploader] = useState({})
    const [pageData, setPageData] = useState({
        arrLiVer: [],
        liVerComponents: [],
        pageIndex: 0,
        pageSize: 4,
        countOfItems: 0,
        current: 1,
    });

    const [documentData, setDocumentData] = useState({
        id: ''
        , wiFiDocumentUrl: ''
        , insuranceDocumentUrl: ''
        , locationDocumentUrl: ''
        , approvedBy: ''
        , createdBy: ''
        , notes: ''
    })
    _logger(Table, documentData)

    useEffect(() => {
        _logger('useEffect...');
        listingVerificationService.getByCreatedBy(pageData.pageIndex, pageData.pageSize)
            .then(onGetLiVerSuccess).catch(onGetLiVerError);
    }, [pageData.pageIndex]);

    const onGetLiVerSuccess = (response) => {
        let arrOfLiVer = response.item.pagedItems;
        setPageData((prevState) => {
            const pd = { ...prevState };
            pd.arrLiVer = arrOfLiVer;
            pd.liVerComponents = arrOfLiVer.map(mapSingleLiVer);
            pd.countOfItems = response.item.totalCount;
            return pd;
        });
    };

    const mapSingleLiVer = (aLiVer) => {
        _logger('mapping...', aLiVer);

        return (
            <ListVerCard
                ListingClicker={onClickedListing}
                listing={aLiVer}
                key={"Friends Id" + aLiVer.id}
            />
        );
    };

    const onClickedListing = useCallback((aListing, eobj) => {
        _logger(eobj)
        const handler = aListing.id
        listingVerificationService //executes service call
            .getById(handler) //gives id param
            .then(onGetVerificationRecordSuccess)
            .catch(onGetVerificationRecordError);
        if (listingSelected === false) {
            setListingSelected(!listingSelected)
        }
        else setListingSelected(!listingSelected);

    });

    const onGetVerificationRecordSuccess = (response => {
        setDocumentData(() => {
            let currentRecord = { ...response.item }
            if (listingSelected === false) {
                setListingSelected(!listingSelected);
            }
            return currentRecord
        })
    })

    const onDocuTypeClicked = (e) => {
        if (e.target.value !== "Select a Document to Upload for your Listing") {
            setDocuTypeUploader(e.target.value)
            if (docuTypeSelected === false) {
                setDocuTypeSelected(!docuTypeSelected)
            }
            return docuTypeUploader
        } else setDocuTypeSelected(!docuTypeSelected)
    }

    const onHandleUploadSuccess = (data) => {
        _logger('File Upload Success', data.items);
    };



    const onGetLiVerError = (err) => {
        _logger('error', err);
    };

    const onGetVerificationRecordError = (err) => {
        _logger('error', err);

    };

    const onPrevNextPage = (page) => {
        setPageData((prevState) => {
            let pg = { ...prevState };
            pg.current = page;
            pg.pageIndex = page - 1;
            return pg;
        });
    };

    return (
        <React.Fragment>
            <>
                <div className="container">
                    <div>
                        <h1 style={{ marginBottom: '50px' }}>
                            TEST TEST TEST
                        </h1>
                    </div>
                    <div className="row">{pageData.liVerComponents}</div>
                </div>
                <div className="row"></div>
                <div style={{ textAlign: 'center', marginBottom: '50px' }}>
                    <Pagination
                        onChange={onPrevNextPage}
                        current={pageData.current}
                        pageSize={pageData.pageSize}
                        total={pageData.countOfItems}
                    />
                </div>
                <div style={{ textAlign: 'center', marginBottom: '5px' }}>
                    {listingSelected && (
                        <div className="col-md-8 offset-md-2">
                            <div className="dropdown py-1 col-md-8 offset-md-2" >
                                <Form.Select onClick={onDocuTypeClicked}>
                                    <option>Select a Document to Upload for your Listing</option>
                                    <option value="WiFiDocumentUrl">Upload a Screenshot of your WiFi Connection Speed</option>
                                    <option value="InsuranceDocumentUrl">Provide your renters or home owners insurance</option>
                                    <option value="LocationDocumentUrl">Provide proof of Location</option>

                                </Form.Select>
                            </div>
                            <div className="col-md-8 offset-md-2">
                                {docuTypeSelected && (
                                    <FileUploader className="col-md-6"
                                        onHandleUploadSuccess={onHandleUploadSuccess} />

                                )}
                            </div>
                            <Table striped bordered hover style={{ marginBotton: "700px" }}>
                                <thead>
                                    <tr>
                                        <th></th>
                                        <th className="col-md-2 text-center">
                                            File Type
                                            <br />
                                        </th>
                                        <th className="col-md-8 text-center">File</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>1.</th>
                                        <td className="text-center">Proof of Ownership or Rental Agreement</td>
                                        <td className="text-center">
                                            <a
                                                href={documentData.wiFiDocumentUrl}
                                                target="_blank"
                                                rel="noopener noreferrer">
                                                {documentData.wiFiDocumentUrl}
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>2.</th>
                                        <td className="text-center">Proof of Home/Rental Insurance</td>
                                        <td className="text-center">
                                            <a
                                                href={documentData.insuranceDocumentUrl}
                                                target="_blank"
                                                rel="noopener noreferrer">
                                                {documentData.insuranceDocumentUrl}
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>3.</th>
                                        <td className="text-center">Wifi Speed Test</td>
                                        <td className="text-center">
                                            <a
                                                href={documentData.locationDocumentUrl}
                                                target="_blank"
                                                rel="noopener noreferrer">
                                                {documentData.locationDocumentUrl}
                                            </a>
                                        </td>
                                    </tr>
                                </tbody>
                            </Table>
                        </div>
                    )}
                </div>
            </>
        </React.Fragment>
    );
}

export default ListingVerification;
